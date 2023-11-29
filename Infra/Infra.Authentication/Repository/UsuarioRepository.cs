using System.Linq.Expressions;
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
        var query = QuerySemaphore<Categoria>(x => x.UsuarioId == usuarioId).Result
                                                                            .OrderBy(x => x.Nome);
        return query.ToList();
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
    
    public async Task<IQueryable<T>> QuerySemaphore<T>(Expression<Func<T, bool>> predicate, 
        bool noTracking = false, params Expression<Func<T, object>>[] includes) where T : class
    {
        await semaphore.WaitAsync();

        try
        {
            var query = _context.Set<T>()
                .Where(predicate)
                .IncludeMultiple(includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
            
        }
        finally
        {
            semaphore.Release();
        }
    }
}