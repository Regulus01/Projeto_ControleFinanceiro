﻿using System.Linq.Expressions;
using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository : IUsuarioRepository
{
    private readonly AuthenticationContext _context;
    private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); // Semáforo estático

    public UsuarioRepository(AuthenticationContext context)
    {
        _context = context;
    }

    public bool EmailCadastrado(string email)
    {
        var emailExiste = _context.Users.FirstOrDefault(x => x.Email == email);
        
        return emailExiste != null;
    }

    public List<Categoria> ObterCategoriasDoUsuario(Guid usuarioId)
    {
        return ObterCategoriasDoUsuarioAsync(usuarioId).Result;
    }

    public async Task<List<Categoria>> ObterCategoriasDoUsuarioAsync(Guid usuarioId)
    {
        await semaphore.WaitAsync();

        try
        {
            var categorias = _context.Categorias
                .Where(x => x.UsuarioId == usuarioId)
                .OrderBy(x => x.Nome)
                .ToList();

            return new List<Categoria>(categorias);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public Usuario? ObterUsuarioPorId(Guid id)
    {
        var user = _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefault(x => x.Id == id);

        return user;
    }
    
    public void AdicionarUsuario(Usuario usuario)
    {
        _context.Add(usuario);
    }
    
    public Categoria? ObterCategoriaPorId(Guid categoriaId)
    {
        return _context.Categorias.FirstOrDefault(x => x.Id == categoriaId);
    }

    public void AdicionarGasto(Gasto gasto)
    {
        _context.Add(gasto);
    }
    
    public void RemoverGasto(Gasto gasto)
    {
        _context.Remove(gasto);
    }
    
    public void Commit()
    {
        _context.SaveChanges();
    }
}