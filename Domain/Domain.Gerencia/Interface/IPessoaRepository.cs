using Domain.Gerencia.Entities;

namespace Domain.Gerencia.Interface;

public interface IPessoaRepository
{
    public void AdicionarPessoa(Pessoa pessoa);
    public void Commit();
}