using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Commands.Notification;
using Domain.Authentication.Entities;
using Infra.Authentication.Interface;
using MediatR;
using SendGrid;
using SendGrid.Helpers.Mail;

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

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (_repository.EmailCadastrado(request.Email))
        {
            return await Task.FromResult("O email cadastrado já existe.");
        }
        
        var user = _mapper.Map<Usuario>(request);
        
        _repository.AdicionarUsuario(user);
        await _mediator.Publish(new UsuarioCriadoNotification { Nome = user.Name, Email = user.Email }, cancellationToken);

        try
        {
            _repository.Commit();
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new UsuarioCriadoNotification { Nome = user.Name, Email = user.Email },
                cancellationToken);
            await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace },
                cancellationToken);

            return await Task.FromResult("Ocorreu um erro no momento do cadastro: ");
        }

        await EnviarEmailDeBoasVidas(user.Email, user.Name);
        return await Task.FromResult("Usuário criado com sucesso.");
    
    }

    private async Task EnviarEmailDeBoasVidas(string userEmail, string userName)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

        if (apiKey == null)
        {
            return;
        }
        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("josecssj.games@gmail.com", "Aline Ramos");
        var to = new EmailAddress(userEmail, userName);

        var userObject = new { username = userName };

        var templateMessage = MailHelper.CreateSingleTemplateEmail(from, to, "d-381280346da44f5794bac24e52acbb5f", userObject);
        await client.SendEmailAsync(templateMessage);
    }
}