using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Authentication.Maps;

public class GastoMap : IEntityTypeConfiguration<Gasto>
{
    public void Configure(EntityTypeBuilder<Gasto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .HasMaxLength(128)
            .HasColumnType("VARCHAR")
            .HasColumnName("Nome");
        
        builder.Property(x => x.Data)
            .HasColumnName("Data");
        
        builder.Property(x => x.Valor)
            .HasColumnName("Valor");
        
        builder.Property(x => x.CategoriaId)
            .HasColumnName("CategoriaId");

        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.Gastos)
            .HasForeignKey(x => x.CategoriaId);
        
        builder.Property(x => x.UsuarioId)
            .HasColumnName("UsuarioId");

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Gastos)
            .HasForeignKey(x => x.UsuarioId);

        builder.ToTable("Gasto", "Autenticacao");
    }
}