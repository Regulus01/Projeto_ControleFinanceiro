using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Authentication.Commands.Token;
using Domain.Authentication.Configuration;
using Infra.Authentication.Interface;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Authentication.Handle;

public class TokenCommandHandler : IRequestHandler<GerarNovoTokenCommand, TokenModel?>
{
    private readonly IUsuarioRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TokenCommandHandler(IUsuarioRepository repository, IMediator mediator, IMapper mapper)
    {
        _repository = repository;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<TokenModel?> Handle(GerarNovoTokenCommand request, CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;
        var refreshToken = request.RefreshToken;
        
        //Valida se o token foi gerado pelo sistema
        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return null;
        }

        //Valida se o refresh foi gerado pelo sistema
        var second = GetPrincipalFromExpiredToken(refreshToken);

        if (second == null)
        {
            return null;
        }
        
        //Gera novo token a partir do id do usuário presente no refresh token
        var userId = Guid.Parse(second.Claims.First().Value);
        var usuario = _repository.ObterUsuarioPorId(userId);

        if (usuario != null)
        {
            var newToken = new TokenService().GenerateToken(usuario);
            return newToken;
        }

        return null;
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.Configuration.JwtKey);
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false,
            ValidIssuer = "Kitandadev.com",
            ValidAudience =  "Artemis.Api",
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
            out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            return null;

        return principal;
    }
}