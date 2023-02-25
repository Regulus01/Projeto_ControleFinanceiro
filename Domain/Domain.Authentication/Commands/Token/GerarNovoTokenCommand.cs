using Domain.Authentication.Configuration;
using MediatR;

namespace Domain.Authentication.Commands.Token;

public class GerarNovoTokenCommand : IRequest<TokenModel?>
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}