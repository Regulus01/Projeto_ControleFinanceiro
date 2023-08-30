using Domain.Gerencia.Entities;

namespace Domain.Gerencia.Interface;

public interface IPessoaRepository
{
    public void AdicionarPessoa(Pessoa pessoa);
    public void AdicionarSaldo(Saldo saldo);
    Guid GetPessoaId(Guid usuarioId);
    public void Commit();
}