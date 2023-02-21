using Application.Authentication.AppService;
using Application.Authentication.AutoMapper;
using Application.Authentication.Interface;
using Domain.Authentication.Commands;
using Infra.Authentication.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Service.Authentication.Repository;


namespace Service.Authentication.DependencyInjection;

public class UsuarioDependencyInjection
{
    public static void Register(IServiceCollection serviceProvider)
    {
        RepositoryDependence(serviceProvider);
    }
    
    private static void RepositoryDependence(IServiceCollection serviceProvider)
    {
        //Auto mapper
        var mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
        serviceProvider.AddSingleton(mapper);
        serviceProvider.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        //Inversao de dependencia
        serviceProvider.AddScoped<IUsuarioRepository, UsuarioRepository>();
        serviceProvider.AddScoped<IUsuarioAppService, UsuarioAppService>();
        
        //Mediatr commands
        serviceProvider.AddMediatR(typeof(RegisterUserCommand).Assembly);
    }
}