using Domain.Authentication.Entities;
using Domain.Authentication.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Authentication.Maps.Shared;

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

        builder.HasOne(x => x.Usuario)
            .WithOne(x => x.Pessoa)
            .HasForeignKey<Usuario>(x => x.Id);

        builder.ToTable("Pessoa", "Gerencia");
    }
}