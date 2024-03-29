﻿// <auto-generated />
using System;
using Infra.Gerencia.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Gerencia.Migrations
{
    [DbContext(typeof(PessoaContext))]
    [Migration("20230319195847_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Gerencia.Entities.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeNascimento");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Endereco");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("Nome");

                    b.Property<int>("Sexo")
                        .HasColumnType("integer")
                        .HasColumnName("Sexo");

                    b.Property<int>("Telefone")
                        .HasColumnType("integer")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Pessoa", "Gerencia");
                });
#pragma warning restore 612, 618
        }
    }
}
