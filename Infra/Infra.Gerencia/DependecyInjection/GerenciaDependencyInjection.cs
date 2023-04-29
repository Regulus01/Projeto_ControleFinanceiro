using System.Data.Common;
using Application.Gerencia.AppService;
using Application.Gerencia.AutoMapper;
using Application.Gerencia.Interface;
using Domain.Gerencia.Commands;
using Domain.Gerencia.Interface;
using Infra.CrossCutting.DependencyInjection;
using Infra.Gerencia.Context;
using Infra.Gerencia.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infra.Gerencia.DependecyInjection;

public class GerenciaDependencyInjection : BaseDependencyInjection
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
        serviceProvider.AddDbContext<PessoaContext>(opt =>
        {
            opt.UseNpgsql(dbConnection, assembly =>
                assembly.MigrationsAssembly(typeof(PessoaContext).Assembly.FullName));
        });

        //Auto mapper
        var mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
        serviceProvider.AddSingleton(mapper);
        serviceProvider.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        //Inversao de dependencia
        serviceProvider.AddScoped<IPessoaRepository, PessoaRepository>();
        serviceProvider.AddScoped<IPessoaAppService, PessoaAppService>();

        //Mediatr commands
        serviceProvider.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(RegisterPessoaCommandGerencia).Assembly);
            
        });
    }
}