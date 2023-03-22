using Application.Gerencia.ViewModels.Pessoa.Enum;

namespace Application.Gerencia.ViewModels.Pessoa;

public class RegistrarPessoaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Telefone { get; set; }
    public string Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public SexoViewModel Sexo { get; set; }

    public Guid UsuarioId { get; set; }
}