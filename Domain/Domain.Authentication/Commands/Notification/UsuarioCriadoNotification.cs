using MediatR;

namespace Domain.Authentication.Commands.Notification;

public class UsuarioCriadoNotification : INotification
{
    public string Nome { get; set; }
    public string Email { get; set; }
}