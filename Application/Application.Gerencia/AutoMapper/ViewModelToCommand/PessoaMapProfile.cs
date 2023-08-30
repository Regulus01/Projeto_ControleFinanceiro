using Application.Gerencia.ViewModels.Pessoa;
using Application.Gerencia.ViewModels.Pessoa.Enum;
using Application.Gerencia.ViewModels.Saldo;
using AutoMapper;
using Domain.Gerencia.Commands;

namespace Application.Gerencia.AutoMapper.ViewModelToCommand;

public class PessoaMapProfile : Profile
{
    public PessoaMapProfile()
    {
        CreateMap<RegistrarPessoaViewModel, RegisterPessoaCommandGerencia>();
        CreateMap<EnderecoViewModel, EnderecoCommand>();
        CreateMap<SaldoViewModel, RegistrarSaldoCommand>();
        CreateMap<SexoViewModel, SexoCommand>();
    }
}