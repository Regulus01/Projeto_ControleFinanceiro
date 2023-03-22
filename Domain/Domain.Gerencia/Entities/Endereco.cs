namespace Domain.Gerencia.Entities;

public class Endereco
{
    public int Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }
    public virtual Pessoa Pessoa { get; set; }
}