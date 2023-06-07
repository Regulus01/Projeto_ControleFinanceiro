using Application.Authentication.ViewModels.Categoria;

namespace Application.Authentication.AppService;

public partial class UsuarioAppService
{
    public List<CategoriaViewModel> ObterCategorias()
    {
        var userId = _user.GetUserId();
        
        if (userId == null)
        {
            return new List<CategoriaViewModel>();
        }
        
        var categorias = _usuarioRepository.ObterCategoriasDoUsuario(userId.Value);

        return _mapper.Map<List<CategoriaViewModel>>(categorias);
    }
}