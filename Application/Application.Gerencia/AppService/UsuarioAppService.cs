using Application.Gerencia.Interface;
using Application.Gerencia.ViewModels.Pessoa;

namespace Application.Gerencia.AppService;

public class UsuarioAppService : IPessoaAppService
{
    public Task<string> RegistrarPessoa(RegistrarPessoaViewModel viewmodel)
    {
        throw new NotImplementedException();
    }
}