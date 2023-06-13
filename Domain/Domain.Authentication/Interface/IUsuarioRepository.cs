using System.Linq.Expressions;
using Domain.Authentication.Entities;

namespace Domain.Authentication.Interface;

public interface IUsuarioRepository
{
    bool EmailCadastrado(string email);
    List<Categoria> ObterCategoriasDoUsuario(Guid usuarioId);
    List<Gasto> ObterGastos(Expression<Func<Gasto, bool>> predicate, int? pagina = 0);
    void AdicionarCategorias(IEnumerable<Categoria> categorias);
    void AdicionarUsuario(Usuario usuario);
    Categoria? ObterCategoriaPorId(Guid categoriaId);
    void AdicionarGasto(Gasto gasto);
    Usuario? ObterUsuarioPorId(Guid id);
    void Commit();
}