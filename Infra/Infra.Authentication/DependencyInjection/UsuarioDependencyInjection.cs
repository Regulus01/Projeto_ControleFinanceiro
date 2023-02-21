using System.Text;
using Application.Authentication.AppService;
using Application.Authentication.AutoMapper;
using Application.Authentication.Interface;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Infra.Authentication.Interface;
using Infra.Authentication.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Authentication.DependencyInjection;

public class UsuarioDependencyInjection
{
    public static void Register(IServiceCollection serviceProvider)
    {
        RepositoryDependence(serviceProvider);
    }
    
    private static void RepositoryDependence(IServiceCollection serviceProvider)
    {
        // Jwt config
        serviceProvider.AddCors();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        serviceProvider.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false
            };                    
        });
        
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
        
        //Token service
        serviceProvider.AddTransient<TokenService>();
    }
}