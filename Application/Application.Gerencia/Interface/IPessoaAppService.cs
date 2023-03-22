using Application.Gerencia.ViewModels.Pessoa;

namespace Application.Gerencia.Interface;

public interface IPessoaAppService
{
    Task<string> RegistrarPessoa(RegistrarPessoaViewModel viewmodel);
}