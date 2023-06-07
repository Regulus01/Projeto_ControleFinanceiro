using Domain.Authentication.Entities;

namespace Domain.Authentication.Interface;

public interface IUsuarioRepository
{
    bool EmailCadastrado(string email);
    List<Categoria> ObterCategoriasDoUsuario(Guid usuarioId);
    void AdicionarCategorias(List<Categoria> categorias);
    void AdicionarUsuario(Usuario usuario);
    Categoria? ObterGastoPorId(Guid categoriaId);
    void AdicionarGasto(Gasto gasto);
    Usuario? ObterUsuarioPorId(Guid id);
    void Commit();
}