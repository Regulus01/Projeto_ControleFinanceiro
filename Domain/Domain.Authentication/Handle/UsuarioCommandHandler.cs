using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Commands.Notification;
using Domain.Authentication.Entities;
using Infra.Authentication.Interface;
using MediatR;

namespace Domain.Authentication.Handle;

public class UsuarioCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IUsuarioRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsuarioCommandHandler(IUsuarioRepository repository, IMediator mediator, IMapper mapper)
    {
        _repository = repository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Usuario>(request);

        try
        {
            _repository.AdicionarPessoaFisica(pessoa);
            _mediator.Publish(new UsuarioCriadoNotification { Nome = user.Name, Email = user.Email },
                cancellationToken);

            _repository.Commit();
            return Task.FromResult("Pessoa fisica Criada.");
        }
        catch (Exception ex)
        {
            _mediator.Publish(new UsuarioCriadoNotification { Nome = user.Name, Email = user.Email },
                cancellationToken);
            _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace },
                cancellationToken);

            return Task.FromResult("Ocorreu um erro no momento do cadastro: ");
        }
    }
}