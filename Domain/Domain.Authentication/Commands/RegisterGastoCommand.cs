using MediatR;

namespace Domain.Authentication.Commands;

public class RegisterGastoCommand : IRequest<string>
{
    public string Nome { get; set; }
    public Guid CategoriaId { get; set; }
    public DateTimeOffset Data { get; set; }
    public TipoDoGastoCommand Tipo { get; set; }
    public double Valor { get;  set; }
    public Guid UsuarioId { get; set; }
}

public enum TipoDoGastoCommand
{
    Entrada = 1,
    Saida = 2,
}