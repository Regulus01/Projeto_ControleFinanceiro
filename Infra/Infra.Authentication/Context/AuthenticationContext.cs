﻿using Domain.Authentication.Entities;
using Infra.Authentication.Maps;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Context;

public class AuthenticationContext : DbContext
{
    public DbSet<Usuario> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Gasto> Gastos { get; set; }
    
    public AuthenticationContext()
    {
    }
    public AuthenticationContext(DbContextOptions<AuthenticationContext> builderOptions) : base(builderOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new GastoMap());
        modelBuilder.ApplyConfiguration(new CategoriaMap());
    }
}