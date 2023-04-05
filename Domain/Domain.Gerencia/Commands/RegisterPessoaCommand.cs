namespace Domain.Gerencia.Commands;

public class RegisterPessoaCommand
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Telefone { get; set; }
    public string Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public SexoViewModel Sexo { get; set; }

    public Guid UsuarioId { get; set; }
}

public enum SexoViewModel : int
{
    Masculino = 1,
    Feminino = 2
}