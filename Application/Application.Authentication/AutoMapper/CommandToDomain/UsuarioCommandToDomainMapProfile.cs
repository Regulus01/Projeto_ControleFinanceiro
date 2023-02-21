using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;
using Infra.CrossCutting.Identify;
using SecureIdentity.Password;

namespace Application.Authentication.AutoMapper.CommandToDomain;

public class UsuarioCommandMapProfile : Profile
{
    public UsuarioCommandMapProfile()
    {
         CreateMap<RegisterUserCommand, Usuario>()
        .ConstructUsing(x => new Usuario(
            x.Name,
            x.Email,
            PasswordHasher.Hash(x.Password, 16, 32, 10000, '.', ""),
            x.Email.Replace("@", "-").Replace(".", "-"),
            RoleIdentify.ClienteRole));
        
    }
}