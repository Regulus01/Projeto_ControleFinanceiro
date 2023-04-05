using Application.Gerencia.ViewModels.Pessoa;
using Infra.Gerencia.Events;

namespace Application.Gerencia.Interface;

public interface IPessoaAppService
{
    Task<PessoaCadastradaEvent> RegistrarPessoa(RegistrarPessoaViewModel viewModel);
}