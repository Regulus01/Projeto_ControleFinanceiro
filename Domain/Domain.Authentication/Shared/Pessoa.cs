using Domain.Authentication.Entities;
using Domain.Authentication.Shared.Enum;

namespace Domain.Authentication.Shared;

public class Pessoa
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public int Telefone { get; set; }
    public string Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public decimal Saldo { get; set; }
    public virtual Usuario Usuario { get; set; }
}