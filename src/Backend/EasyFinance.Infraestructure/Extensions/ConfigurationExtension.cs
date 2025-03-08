using Microsoft.Extensions.Configuration;

namespace EasyFinance.Infraestructure.Extensions;
public static class ConfigurationExtension
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("MySqlConnection")!;
    }
}

