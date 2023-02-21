using MediatR;

namespace Domain.Authentication.Commands.Notification;

public class ErroNotification : INotification
{
    public string Excecao { get; set; }
    public string? PilhaErro { get; set; }
}