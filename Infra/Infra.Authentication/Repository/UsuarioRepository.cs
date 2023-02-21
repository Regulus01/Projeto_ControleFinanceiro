using Domain.Authentication.Entities;
using Infra.Authentication.Context;
using Infra.Authentication.Interface;

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

    public void AdicionarUsuario(Usuario usuario)
    {
        _context.Add(usuario);
    }
    public void Commit()
    {
        _context.SaveChanges();
    }
}