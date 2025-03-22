using EasyFinance.Infraestructure;
using EasyFinance.Application;
using EasyFinance.API.Filters;
using EasyFinance.Infraestructure.Migrations;
using EasyFinance.Infraestructure.Extensions;
using EasyFinance.Domain.Security.Tokens;
using EasyFinance.API.Token;
using EasyFinance.API.BackgroundServices;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
const string AUTHENTICATION_TYPE = "Bearer";

builder.Services.AddControllers();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 123456789abcefgh'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AUTHENTICATION_TYPE,
                },
                Scheme = "oauth2",
                Name = AUTHENTICATION_TYPE,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddHostedService<IncreaseRecurrentBillsService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    var connectionString = builder.Configuration.ConnectionString();

    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DatabaseMigration.Migrate(serviceScope.ServiceProvider, connectionString);
}

public partial class Program
{
    protected Program() 
    {

    }
}
