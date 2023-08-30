using Domain.Gerencia.Entities;
using Domain.Gerencia.Interface;
using Infra.Gerencia.Context;
using Microsoft.EntityFrameworkCore;

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

    public void AdicionarSaldo(Saldo saldo)
    {
        _context.Add(saldo);
    }

    public Guid GetPessoaId(Guid usuarioId)
    {
        var pessoa = _context.Pessoas.Include(x => x.Usuario).FirstOrDefault(x => x.Usuario.Id == usuarioId);
        if (pessoa != null) 
            return pessoa.Id;

        return Guid.Empty;
    }
    
    public void Commit()
    {
        _context.SaveChanges();
    }
}

