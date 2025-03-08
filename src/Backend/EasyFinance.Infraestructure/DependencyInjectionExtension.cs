using EasyFinance.Domain.Repositories;
using EasyFinance.Domain.Repositories.User;
using EasyFinance.Infraestructure.DataAccess;
using EasyFinance.Infraestructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyFinance.Infraestructure;
public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MySqlConnection")!;

        services.AddDbContext<EasyFinanceDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.Create(new Version(8,0,37), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
}

