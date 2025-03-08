using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Domain.Security.Cryptography;
using EasyFinance.Infraestructure.DataAccess;
using EasyFinance.Infraestructure.DataAccess.Repositories;
using EasyFinance.Infraestructure.Extensions;
using EasyFinance.Infraestructure.Security.Cryptography;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyFinance.Infraestructure;
public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddFluentMigrator(services, configuration);
        AddPasswordEncrypter(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddDbContext<EasyFinanceDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.Create(new Version(8,0,37), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));
        });
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddMySql8()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("EasyFinance.Infraestructure")).For.All();
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }


    private static void AddPasswordEncrypter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
    }
}

