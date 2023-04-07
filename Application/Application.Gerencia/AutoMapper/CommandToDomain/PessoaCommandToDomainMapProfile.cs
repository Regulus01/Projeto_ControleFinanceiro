using AutoMapper;
using Domain.Gerencia.Commands;
using Domain.Gerencia.Entities;
using Domain.Gerencia.Enum;

namespace Application.Gerencia.AutoMapper.CommandToDomain;

public class PessoaCommandToDomainMapProfile : Profile
{
    public PessoaCommandToDomainMapProfile()
    {
        CreateMap<RegisterPessoaCommand, Pessoa>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<EnderecoCommand, Endereco>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<SexoCommand, Sexo>();
    }
}