using Application.Authentication.AutoMapper.CommandToDomain;
using AutoMapper;

namespace Application.Authentication.AutoMapper;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile<UsuarioMapProfile>();
        });
    }
}