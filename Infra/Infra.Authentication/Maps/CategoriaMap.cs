using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Authentication.Maps;

public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .HasMaxLength(128)
            .HasColumnType("VARCHAR")
            .HasColumnName("Nome");
        
        builder.Property(x => x.UsuarioId)
            .HasColumnName("UsuarioId");

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Categorias)
            .HasForeignKey(x => x.UsuarioId);
        
        builder.ToTable("Categoria", "Autenticacao");
    }
}