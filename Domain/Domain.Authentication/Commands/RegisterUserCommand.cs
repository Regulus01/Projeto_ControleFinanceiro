using MediatR;

namespace Domain.Authentication.Commands;

public class RegisterUserCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RegisterPessoaCommand Pessoa { get; set; }

}

public class RegisterPessoaCommand
{
    public string Nome { get; set; }
    public long Telefone { get; set; }
    public EnderecoCommand? Endereco { get; set; }
    public DateTime? DataDeNascimento { get; set; }
    public SexoCommand? Sexo { get; set; }
}

public class EnderecoCommand {
    
    public int Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }

}
public enum SexoCommand : int
{
    Masculino = 1,
    Feminino = 2
}