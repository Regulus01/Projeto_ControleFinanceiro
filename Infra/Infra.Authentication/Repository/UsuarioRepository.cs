using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public class UsuarioRepository : IUsuarioRepository
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
    
    public void Commit()
    {
        _context.SaveChanges();
    }
}