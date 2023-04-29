using AutoMapper;
using Domain.Gerencia.Commands;
using Domain.Gerencia.Entities;
using Domain.Gerencia.Interface;
using Infra.Gerencia.Events;
using MediatR;

namespace Domain.Gerencia.Handle;

public class PessoaCommandHandler : IRequestHandler<RegisterPessoaCommandGerencia, PessoaCadastradaEvent>
{
    private readonly IPessoaRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PessoaCommandHandler(IPessoaRepository repository, IMediator mediator, IMapper mapper)
    {
        _repository = repository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public Task<PessoaCadastradaEvent> Handle(RegisterPessoaCommandGerencia request, CancellationToken cancellationToken)
    {
        var pessoa = _mapper.Map<Pessoa>(request);
        
        _repository.AdicionarPessoa(pessoa);

        _repository.Commit();
        
        var pessoaEvent = new PessoaCadastradaEvent
        {
            PessoaId = pessoa.Id,
            Nome = pessoa.Nome
        };
        
        return Task.FromResult(pessoaEvent);
    }
}