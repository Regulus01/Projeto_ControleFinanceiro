using Domain.Authentication.Entities;
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

        builder.Property(x => x.EnderecoId)
            .HasColumnName("id_endereco")
            .IsRequired();
        
        builder.HasOne(x => x.Endereco)
            .WithOne(x => x.Pessoa)
            .HasForeignKey<Pessoa>(x => x.EnderecoId);

        builder.HasOne(x => x.Usuario)
            .WithOne(x => x.Pessoa)
            .HasForeignKey<Usuario>(x => x.Id);
        
        builder.ToTable("Pessoa", "Gerencia");
    }
}