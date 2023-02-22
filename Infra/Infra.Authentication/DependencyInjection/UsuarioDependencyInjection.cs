using System.Data.Common;
using Application.Authentication.AppService;
using Application.Authentication.AutoMapper;
using Application.Authentication.Interface;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Infra.Authentication.Context;
using Infra.Authentication.Interface;
using Infra.Authentication.Repository;
using Infra.CrossCutting.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infra.Authentication.DependencyInjection;

public class UsuarioDependencyInjection : BaseDependencyInjection
{
    public static void Register(IServiceCollection serviceProvider)
    {
        RepositoryDependence(serviceProvider);
    }

    private static void RepositoryDependence(IServiceCollection serviceProvider)
    {
        BaseRepositoryDependence(serviceProvider);
        
        //DBConnection
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("Config/appsettings.json") // Obtem o appsettings da pasta de configuracao
            .Build(); 

        DbConnection dbConnection = new NpgsqlConnection(configuration.GetConnectionString("app"));
        //Para adicionar mais contextos é necessário repetir o addDbContext
        serviceProvider.AddDbContext<AuthenticationContext>(opt =>
        {
            opt.UseNpgsql(dbConnection, assembly =>
                assembly.MigrationsAssembly(typeof(AuthenticationContext).Assembly.FullName));
        });

        //Token service
        serviceProvider.AddTransient<TokenService>();
        
        //Auto mapper
        var mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
        serviceProvider.AddSingleton(mapper);
        serviceProvider.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        //Inversao de dependencia
        serviceProvider.AddScoped<IUsuarioRepository, UsuarioRepository>();
        serviceProvider.AddScoped<IUsuarioAppService, UsuarioAppService>();
        
        //Mediatr commands
        serviceProvider.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(RegisterUserCommand).Assembly);
            
        });
        
        
    }
}