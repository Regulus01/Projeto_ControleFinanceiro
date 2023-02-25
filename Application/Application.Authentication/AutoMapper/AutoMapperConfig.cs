using Application.Authentication.AutoMapper.CommandToDomain;
using Application.Authentication.AutoMapper.ViewModelToCommand;
using AutoMapper;

namespace Application.Authentication.AutoMapper;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile<UsuarioCommandMapProfile>();
            config.AddProfile<UsuarioViewModelCommandMapProfile>();
            config.AddProfile<TokenViewModelCommandMapProfile>();
        });
    }
}