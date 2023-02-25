using Application.Authentication.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands.Token;

namespace Application.Authentication.AutoMapper.ViewModelToCommand;

public class TokenViewModelCommandMapProfile : Profile
{
    public TokenViewModelCommandMapProfile()
    {
        CreateMap<TokenViewModel, GerarNovoTokenCommand>();
    }
}