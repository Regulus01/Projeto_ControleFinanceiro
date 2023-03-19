using Domain.Gerencia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Gerencia.Maps;

public class PessoaMap : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .HasMaxLength(128)
            .HasColumnName("Nome");

        builder.Property(x => x.DataDeNascimento)
            .HasColumnName("DataDeNascimento");

        builder.Property(x => x.Telefone)
            .HasColumnName("Telefone");

        builder.Property(x => x.Sexo)
            .HasColumnName("Sexo");

        builder.Property(x => x.Endereco)
            .HasColumnName("Endereco");
        
        builder.ToTable("Pessoa", "Gerencia");
    }
}