using System.Data.Common;
using Application.Authentication.AppService;
using Application.Authentication.AutoMapper;
using Application.Authentication.Interface;
using Application.Gerencia.AppService;
using Application.Gerencia.Interface;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Domain.Authentication.Interface;
using Domain.Gerencia.Commands;
using Domain.Gerencia.Interface;
using Infra.Authentication.Context;
using Infra.Authentication.Repository;
using Infra.CrossCutting.DependencyInjection;
using Infra.Gerencia.Context;
using Infra.Gerencia.Repository;
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
        
        //ToDo: Remover após resolver o problema com a azure  
        serviceProvider.AddDbContext<PessoaContext>(opt =>
        {
            opt.UseNpgsql(dbConnection, assembly =>
                assembly.MigrationsAssembly(typeof(PessoaContext).Assembly.FullName));
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
        serviceProvider.AddScoped<ITokenAppService, TokenAppService>();
        
        
        //ToDo: Remover após resolver o problema com a azure  
        serviceProvider.AddScoped<IPessoaRepository, PessoaRepository>();
        serviceProvider.AddScoped<IPessoaAppService, PessoaAppService>();
        
        //Mediatr commands
        serviceProvider.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(RegisterUserCommand).Assembly);
            //ToDo: Remover após resolver o problema com a azure 
            config.RegisterServicesFromAssemblies(typeof(RegisterPessoaCommandGerencia).Assembly);
        });


    }
}