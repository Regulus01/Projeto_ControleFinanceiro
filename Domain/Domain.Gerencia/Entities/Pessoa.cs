using Domain.Gerencia.Enum;

namespace Domain.Gerencia.Entities;

public class Pessoa
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public int Telefone { get; set; }
    public string Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Sexo Sexo { get; set; }
}