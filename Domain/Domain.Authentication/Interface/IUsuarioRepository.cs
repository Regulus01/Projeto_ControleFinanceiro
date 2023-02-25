using Domain.Authentication.Entities;

namespace Infra.Authentication.Interface;

public interface IUsuarioRepository
{
    bool EmailCadastrado(string email);
    void AdicionarUsuario(Usuario usuario);
    Usuario? ObterUsuarioPorId(Guid id);
    void Commit();
}