using AutoMapper;
using Domain.Gerencia.Commands;
using Domain.Gerencia.Entities;
using Domain.Gerencia.Interface;
using Infra.CrossCutting.Interface;
using Infra.Gerencia.Events;
using MediatR;

namespace Domain.Gerencia.Handle;

public class PessoaCommandHandler : IRequestHandler<RegisterPessoaCommandGerencia, PessoaCadastradaEvent>,
                                    IRequestHandler<RegistrarSaldoCommand, string>
{
    private readonly IPessoaRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IAuthenticatedUser _user;

    public PessoaCommandHandler(IPessoaRepository repository, IMediator mediator, IMapper mapper, IAuthenticatedUser user)
    {
        _repository = repository;
        _mediator = mediator;
        _mapper = mapper;
        _user = user;
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

    public Task<string> Handle(RegistrarSaldoCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine();
        var idUsuario = _user.GetUserId();

        if (idUsuario == null)
        {
            return null;
        }
        var pessoaId = _repository.GetPessoaId(idUsuario.Value);

        var saldo = _mapper.Map<Saldo>(request);
        saldo.DataInsercao = DateTimeOffset.UtcNow;
        saldo.Tipo = Tipo.Entrada;
        saldo.PessoaId = pessoaId;
        _repository.AdicionarSaldo(saldo);
        
        _repository.Commit();
        
        return Task.FromResult("Sucesso");
    }
}