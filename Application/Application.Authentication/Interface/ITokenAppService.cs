using Application.Authentication.ViewModels;
using Domain.Authentication.Configuration;

namespace Application.Authentication.Interface;

public interface ITokenAppService
{
    Task<TokenModel?> GerarNovoToken(TokenViewModel? tokenModel);
}