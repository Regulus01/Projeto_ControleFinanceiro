using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;
using Infra.CrossCutting.Identify;
using SecureIdentity.Password;

namespace Application.Authentication.AutoMapper.CommandToDomain;

public class UsuarioMapProfile : Profile
{
    public UsuarioMapProfile()
    {
         CreateMap<RegisterUserCommand, Usuario>()
        .ConstructUsing(x => new Usuario(
            x.Name,
            x.Email,
            x.Email.Replace("@", "-").Replace(".", "-"),
            PasswordHasher.Hash(x.Password, 16, 32, 10000, '.', ""),
            RoleIdentify.ClienteRole));
    }
}