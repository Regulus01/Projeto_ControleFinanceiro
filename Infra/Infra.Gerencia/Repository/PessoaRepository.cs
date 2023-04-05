using Domain.Gerencia.Entities;
using Domain.Gerencia.Interface;
using Infra.Gerencia.Context;

namespace Infra.Gerencia.Repository;

public class PessoaRepository : IPessoaRepository
{
    private readonly PessoaContext _context;
    
    public PessoaRepository(PessoaContext context)
    {
        _context = context;
    }
    public void AdicionarPessoa(Pessoa pessoa)
    {
        _context.Add(pessoa);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}

