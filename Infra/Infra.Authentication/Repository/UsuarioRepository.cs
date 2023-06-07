using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository : IUsuarioRepository
{
    private readonly AuthenticationContext _context;

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
        var categorias = _context.Categorias
            .Where(x => x.UsuarioId == usuarioId)
            .OrderBy(x => x.Nome);
        
        return new List<Categoria>(categorias);
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

    public Categoria? ObterGastoPorId(Guid categoriaId)
    {
        return _context.Categorias.FirstOrDefault(x => x != null && x.Id == categoriaId);
    }

    public void AdicionarGasto(Gasto gasto)
    {
        _context.Add(gasto);
    }
    
    public void Commit()
    {
        _context.SaveChanges();
    }
}