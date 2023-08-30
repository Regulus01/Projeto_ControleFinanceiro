using MediatR;

namespace Domain.Gerencia.Commands;

public class RegistrarSaldoCommand : IRequest<string>
{
    public decimal Valor { get; set; }
}