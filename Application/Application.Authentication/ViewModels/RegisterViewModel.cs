using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must be valid")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    public RegistrarPessoaViewModel Pessoa { get; set; }
}

public class RegistrarPessoaViewModel
{
    public string Nome { get; set; }
    public int Telefone { get; set; }
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

public enum SexoViewModel : int
{
    Masculino = 1,
    Feminino = 2
}