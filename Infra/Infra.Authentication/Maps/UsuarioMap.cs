using Domain.Authentication.Entities;
using Domain.Authentication.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Authentication.Maps;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.PessoaId)
            .HasColumnName("PessoaId");

        builder.HasOne(x => x.Pessoa)
            .WithOne(x => x.Usuario)
            .HasForeignKey<Usuario>(x => x.PessoaId);
            
        builder.HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique();
        
        builder.ToTable("Usuario", "Autenticacao");
    }
}