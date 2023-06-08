using Application.Gerencia.ViewModels.Pessoa.Enum;

namespace Application.Gerencia.ViewModels.Pessoa;

public class RegistrarPessoaViewModel
{
    public string Nome { get; set; }
    public long Telefone { get; set; }
    public EnderecoViewModel Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public SexoViewModel Sexo { get; set; }
}

public class EnderecoViewModel 
{
    public int Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }

}