using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Commands.Notification;
using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using MediatR;

namespace Domain.Authentication.Handle;

public partial class UsuarioCommandHandler : IRequestHandler<RegisterUserCommand, string>
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

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (_repository.EmailCadastrado(request.Email))
        {
            return "O email cadastrado já existe.";
        }

        var response = await RegistrarPessoaAnotherService(request.Pessoa);

        
        if (response == null)
            return "Ocorreu um erro no momento do cadastro";
        
        var user = _mapper.Map<Usuario>(request);
        
        if (response.PessoaId != null) 
            user.InformePessoaId(response.PessoaId.Value);
        else
            return "Ocorreu um erro no momento do cadastro";

        _repository.AdicionarUsuario(user);
        
        //Categoria 
        var categorias = new List<Categoria>
        {
            new Categoria(Guid.NewGuid(), "Alimentação", user.Id ),
            new Categoria(Guid.NewGuid(), "Moradia ", user.Id ),
            new Categoria(Guid.NewGuid(), "Transporte ", user.Id ),
            new Categoria(Guid.NewGuid(), "Saúde ", user.Id ),
            new Categoria(Guid.NewGuid(), "Educação ", user.Id ),
            new Categoria(Guid.NewGuid(), "Lazer e entretenimento ", user.Id ),
            new Categoria(Guid.NewGuid(), "Vestuário e acessórios", user.Id ),
            new Categoria(Guid.NewGuid(), "Outros", user.Id ),
        };
        
        _repository.AdicionarCategorias(categorias);
        
        try
        {
            _repository.Commit();
            Console.WriteLine("Usuario criado com sucesso " + request.Email);
            await _mediator.Publish(new UsuarioCriadoNotification { Nome = response.Nome, Email = user.Email }, cancellationToken);
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new UsuarioCriadoNotification { Nome = user.Name, Email = user.Email },
                cancellationToken);
            await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace },
                cancellationToken);

            return "Ocorreu um erro no momento do cadastro";
        }

        await EnviarEmailDeBoasVidas(user.Email, user.Name);
        return "Usuário criado com sucesso.";
    
    }
}