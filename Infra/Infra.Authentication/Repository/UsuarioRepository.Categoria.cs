using Domain.Authentication.Entities;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository
{
    public void AdicionarCategorias(List<Categoria> categorias)
    {
        _context.AddRange(categorias);
    }
}