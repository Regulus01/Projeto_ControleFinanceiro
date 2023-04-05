using Domain.Gerencia.Entities;
using Infra.Gerencia.Maps;
using Microsoft.EntityFrameworkCore;

namespace Infra.Gerencia.Context;

public class PessoaContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }

    public PessoaContext()
    {
    }
    public PessoaContext(DbContextOptions<PessoaContext> builderOptions) : base(builderOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PessoaMap());
        modelBuilder.ApplyConfiguration(new EnderecoMap());
    }
}