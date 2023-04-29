using Application.Authentication.AutoMapper.CommandToDomain;
using Application.Authentication.AutoMapper.ViewModelToCommand;
using Application.Gerencia.AutoMapper.CommandToDomain;
using Application.Gerencia.AutoMapper.ViewModelToCommand;
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
            
            //ToDo: Remover após resolver o problema com a azure  
            config.AddProfile<PessoaMapProfile>();
            config.AddProfile<PessoaCommandToDomainMapProfile>();
        });
    }
}