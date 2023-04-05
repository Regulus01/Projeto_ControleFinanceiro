using Domain.Gerencia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Gerencia.Maps;

public class EnderecoMap : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Cep)
            .HasMaxLength(8)
            .HasColumnName("Cep");

        builder.Property(x => x.Logradouro)
            .HasColumnName("Logradouro");

        builder.Property(x => x.Localidade)
            .HasColumnName("Localidade");

        builder.Property(x => x.Bairro)
            .HasColumnName("Bairro");

        builder.Property(x => x.Uf)
            .HasColumnName("Uf");

        builder.ToTable("Endereco", "Gerencia");
    }
}