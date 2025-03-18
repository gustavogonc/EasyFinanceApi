using EasyFinance.Application.Services.AutoMapper;
using EasyFinance.Application.UseCases.Expense.Register;
using EasyFinance.Application.UseCases.Login;
using EasyFinance.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyFinance.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        {
            autoMapperOptions.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
    }
}

