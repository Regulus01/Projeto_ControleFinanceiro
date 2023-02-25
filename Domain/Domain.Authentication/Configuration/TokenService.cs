using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Authentication.Entities;
using Domain.Authentication.Extension;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Authentication.Configuration;

public class TokenService
{
    public TokenModel GenerateToken (Usuario user)
    {
        //Estancia do manipulador de Token
        var tokenHandler = new JwtSecurityTokenHandler();

        var userClaims = user.GetClaims().ToList();
        
        //Gerando o token
        var accessToken = tokenHandler.CreateToken(GerarToken(8, userClaims));
        var accessTokenExpiration = accessToken.ValidTo;
        var refreshToken = tokenHandler.CreateToken(GerarToken(10, userClaims));
        var refreshTokenExpiration = refreshToken.ValidTo;

        var tokenModel = new TokenModel(tokenHandler.WriteToken(accessToken), accessTokenExpiration, 
                                        tokenHandler.WriteToken(refreshToken), refreshTokenExpiration);
        
        //Retornando tudo como uma string
        return tokenModel;
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
           

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
            out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
    
    public SecurityTokenDescriptor GerarToken(int tempo, IEnumerable<Claim> userClaims)
    {
        //Chave da classe Configuration. O Token Handler espera um Array de Bytes, por isso é necessário converter
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims), //Claims que vão compor o token
            Expires = DateTime.UtcNow.AddHours(tempo), //Por quanto tempo vai valer o token?
            SigningCredentials = //Assinatura do token, serve para identificar que mandou o
                                 //token e garantir que o token não foi alterado no meio do caminho.
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }
}