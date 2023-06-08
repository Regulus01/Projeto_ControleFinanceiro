using Domain.Gerencia.Enum;
using Domain.Gerencia.Shared.Usuario;

namespace Domain.Gerencia.Entities;

public class Pessoa
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public long Telefone { get; private set; }
    public Guid EnderecoId { get; private set; }
    public virtual Endereco Endereco { get; private set; }
    public DateTime DataDeNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public virtual Usuario Usuario { get; private set; }
    
    public void InformeEnderecoId(Guid id)
    {
        EnderecoId = id;
    }
}