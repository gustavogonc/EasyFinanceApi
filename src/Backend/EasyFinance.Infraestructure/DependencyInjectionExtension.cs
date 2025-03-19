using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.Expense;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Domain.Security.Cryptography;
using EasyFinance.Domain.Security.Tokens;
using EasyFinance.Domain.Services.LoggedUser;
using EasyFinance.Infraestructure.DataAccess;
using EasyFinance.Infraestructure.DataAccess.Repositories;
using EasyFinance.Infraestructure.Extensions;
using EasyFinance.Infraestructure.Security.Cryptography;
using EasyFinance.Infraestructure.Security.Tokens.Access.Generator;
using EasyFinance.Infraestructure.Security.Tokens.Access.Validator;
using EasyFinance.Infraestructure.Services;
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
        AddTokens(services, configuration);
        AddServices(services);
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

        services.AddScoped<IExpenseWriteOnlyRepository, ExpenseRepository>();
        services.AddScoped<IExpenseReadOnlyRepository, ExpenseRepository>();
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Jwt:ExpirationTimeMinutes");
        var signinKey = configuration.GetValue<string>("Jwt:SigninKey");

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signinKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signinKey!));
    }


    private static void AddPasswordEncrypter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }
}

