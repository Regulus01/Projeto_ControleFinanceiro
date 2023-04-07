using Infra.Gerencia.Events;
using MediatR;

namespace Domain.Gerencia.Commands;

public class RegisterPessoaCommand : IRequest<PessoaCadastradaEvent>
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Telefone { get; set; }
    public EnderecoCommand Endereco { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public SexoCommand Sexo { get; set; }
}

public class EnderecoCommand {
    
    public Guid Id { get; set; }
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