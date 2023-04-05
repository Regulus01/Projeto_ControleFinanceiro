using Application.Gerencia.ViewModels.Pessoa;
using AutoMapper;
using Domain.Gerencia.Commands;

namespace Application.Gerencia.AutoMapper.ViewModelToCommand;

public class PessoaMapProfile : Profile
{
    public PessoaMapProfile()
    {
        CreateMap<RegistrarPessoaViewModel, RegisterPessoaCommand>();
    }
}