using Domain.Gerencia.Enum;
using Domain.Gerencia.Shared.Usuario;

namespace Domain.Gerencia.Entities;

public class Pessoa
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Telefone { get; set; }
    public Guid EnderecoId { get; set; }
    public Endereco Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public virtual Usuario Usuario { get; set; }
}