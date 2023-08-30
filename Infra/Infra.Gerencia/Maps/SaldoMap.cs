using Domain.Gerencia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Gerencia.Maps;

public class SaldoMap : IEntityTypeConfiguration<Saldo>
{
    public void Configure(EntityTypeBuilder<Saldo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Valor)
            .HasColumnName("Valor");

        builder.Property(x => x.DataInsercao)
            .HasColumnName("DataInsercao");

        builder.Property(x => x.Tipo)
            .HasColumnName("Tipo");
        
        builder.HasOne<Pessoa>()
            .WithMany(x => x.Saldo)
            .HasForeignKey(x => x.Id);
        
        builder.ToTable("Saldo", "Gerencia");
    }
}