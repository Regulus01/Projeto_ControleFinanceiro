namespace Domain.Gerencia.Entities;

public class Saldo
{
    public Guid Id { get; private set; }
    public decimal Valor { get; private set; }
    public DateTimeOffset DataInsercao { get; set; }
    public Tipo Tipo { get;  set; }
    public Guid PessoaId { get ; set; }
    public Pessoa Pessoa { get; set; }
}

public enum Tipo : int
{
    Entrada = 1,
    Saida = 2
}