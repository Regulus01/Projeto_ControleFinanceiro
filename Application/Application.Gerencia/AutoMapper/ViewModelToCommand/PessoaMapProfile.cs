using Application.Gerencia.ViewModels.Pessoa;
using Application.Gerencia.ViewModels.Pessoa.Enum;
using AutoMapper;
using Domain.Gerencia.Commands;

namespace Application.Gerencia.AutoMapper.ViewModelToCommand;

public class PessoaMapProfile : Profile
{
    public PessoaMapProfile()
    {
        CreateMap<RegistrarPessoaViewModel, RegisterPessoaCommandGerencia>();
        CreateMap<EnderecoViewModel, EnderecoCommand>();
        CreateMap<SexoViewModel, SexoCommand>();
    }
}