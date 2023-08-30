using Application.Gerencia.Interface;
using Application.Gerencia.ViewModels.Pessoa;
using Application.Gerencia.ViewModels.Saldo;
using AutoMapper;
using Domain.Gerencia.Commands;
using Infra.CrossCutting.Interface;
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
        var pessoa = _mapper.Map<RegisterPessoaCommandGerencia>(viewModel);

        var evento = _mediator.Send(pessoa);

        return evento;
    }
    
    public async Task<string> InserirSaldo(SaldoViewModel viewModel)
    {
        if (viewModel.Valor  <= 0)
            return "O valor precisa ser maior que 0.";
        
        var command = _mapper.Map<RegistrarSaldoCommand>(viewModel);
        
        var result = await _mediator.Send(command);

        return result;

    }
}