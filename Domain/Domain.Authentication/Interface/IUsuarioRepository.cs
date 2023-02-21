using Domain.Authentication.Entities;

namespace Infra.Authentication.Interface;

public interface IUsuarioRepository
{
    bool EmailCadastrado(string email);
    void AdicionarUsuario(Usuario usuario);
    void Commit();
}