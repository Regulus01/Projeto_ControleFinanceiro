using MediatR;

namespace Domain.Authentication.Commands.Notification;

public class GastoCriadoNotification : INotification
{
    public string Nome { get; set; }
}