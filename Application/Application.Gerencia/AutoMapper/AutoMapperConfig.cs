using Application.Gerencia.AutoMapper.CommandToDomain;
using Application.Gerencia.AutoMapper.ViewModelToCommand;
using AutoMapper;

namespace Application.Gerencia.AutoMapper;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile<PessoaMapProfile>();
            config.AddProfile<PessoaCommandToDomainMapProfile>();
        });
    }
}