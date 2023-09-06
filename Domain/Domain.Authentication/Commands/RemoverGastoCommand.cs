using MediatR;

namespace Domain.Authentication.Commands;

public class RemoverGastoCommand : IRequest<string>
{
    public Guid GastoId { get; set; }
    public Guid UsuarioId { get; set; }

    public RemoverGastoCommand(Guid gastoId, Guid usuarioId)
    {
        GastoId = gastoId;
        UsuarioId = usuarioId;
    }
}