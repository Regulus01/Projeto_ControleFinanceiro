﻿// <auto-generated />
using System;
using Infra.Gerencia.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Gerencia.Migrations
{
    [DbContext(typeof(PessoaContext))]
    partial class PessoaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Gerencia.Entities.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Bairro");

                    b.Property<int>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("integer")
                        .HasColumnName("Cep");

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Localidade");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Logradouro");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Uf");

                    b.HasKey("Id");

                    b.ToTable("Endereco", "Gerencia");
                });

            modelBuilder.Entity("Domain.Gerencia.Entities.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeNascimento");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_endereco");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("Nome");

                    b.Property<int>("Sexo")
                        .HasColumnType("integer")
                        .HasColumnName("Sexo");

                    b.Property<long>("Telefone")
                        .HasColumnType("bigint")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.ToTable("Pessoa", "Gerencia");
                });

            modelBuilder.Entity("Domain.Gerencia.Shared.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Domain.Gerencia.Shared.Usuario.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PessoaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Domain.Gerencia.Entities.Pessoa", b =>
                {
                    b.HasOne("Domain.Gerencia.Entities.Endereco", "Endereco")
                        .WithOne("Pessoa")
                        .HasForeignKey("Domain.Gerencia.Entities.Pessoa", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Domain.Gerencia.Shared.Usuario.Usuario", b =>
                {
                    b.HasOne("Domain.Gerencia.Entities.Pessoa", "Pessoa")
                        .WithOne("Usuario")
                        .HasForeignKey("Domain.Gerencia.Shared.Usuario.Usuario", "PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Gerencia.Shared.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Gerencia.Entities.Endereco", b =>
                {
                    b.Navigation("Pessoa")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Gerencia.Entities.Pessoa", b =>
                {
                    b.Navigation("Usuario")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
