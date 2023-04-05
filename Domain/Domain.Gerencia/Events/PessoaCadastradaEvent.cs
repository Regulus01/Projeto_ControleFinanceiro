namespace Infra.Gerencia.Events;

public class PessoaCadastradaEvent
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; }
}