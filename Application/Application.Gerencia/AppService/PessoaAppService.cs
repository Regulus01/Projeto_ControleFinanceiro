using Application.Gerencia.Interface;
using Application.Gerencia.ViewModels.Pessoa;
using AutoMapper;
using Domain.Gerencia.Commands;
using Infra.Gerencia.Events;
using MediatR;

namespace Application.Gerencia.AppService;

public class PessoaAppService : IPessoaAppService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public PessoaAppService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public Task<PessoaCadastradaEvent> RegistrarPessoa(RegistrarPessoaViewModel viewModel)
    {
        var pessoa = _mapper.Map<RegisterPessoaCommand>(viewModel);

        var evento = _mediator.Send(pessoa);

        return evento;
    }
}