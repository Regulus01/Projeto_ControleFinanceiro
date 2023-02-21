using Application.Authentication.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands;

namespace Application.Authentication.AutoMapper.ViewModelToCommand;

public class UsuarioViewModelCommandMapProfile : Profile
{
    public UsuarioViewModelCommandMapProfile()
    {
        CreateMap<RegisterViewModel, RegisterUserCommand>();
    }
}