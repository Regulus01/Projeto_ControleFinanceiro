using Application.Gerencia.ViewModels.Pessoa;
using Application.Gerencia.ViewModels.Saldo;
using Infra.Gerencia.Events;

namespace Application.Gerencia.Interface;

public interface IPessoaAppService
{
    Task<PessoaCadastradaEvent> RegistrarPessoa(RegistrarPessoaViewModel viewModel);
    Task<string> InserirSaldo(SaldoViewModel viewModel);
    
}