using System.Text;
using Domain.Authentication.Configuration;
using Infra.CrossCutting.User.Athenticated;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infra.CrossCutting.DependencyInjection;

public class BaseDependencyInjection
{
    protected static void BaseRepositoryDependence(IServiceCollection serviceProvider)
    {
        serviceProvider.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        serviceProvider.AddScoped<AuthenticatedUser>();

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
        
        //Token service
        serviceProvider.AddTransient<TokenService>();
    }
}