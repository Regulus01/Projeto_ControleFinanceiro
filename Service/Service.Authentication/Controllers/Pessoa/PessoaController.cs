using Application.Authentication.ViewModels;
using Application.Gerencia.Interface;
using Domain.Authentication.Shared.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrarPessoaViewModel2 = Application.Gerencia.ViewModels.Pessoa.RegistrarPessoaViewModel;

namespace Service.Authentication.Controllers.Pessoa;

[Authorize]
[Route("api/Pessoa")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IPessoaAppService _appService;

    public PessoaController(IPessoaAppService appService)
    {
        _appService = appService;
    }

    /// <summary>
    ///     EndPoint de uso do backend
    /// </summary>
    /// <remarks>
    ///     EndPoint Sem autorização utilizado para cadastrar a pessoa no Sistema
    /// </remarks>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("v1/RegistrarPessoa")]
    public Task<IActionResult> RegistrarPessoa([FromBody] RegistrarPessoaViewModel viewModel)
    {
        var sexoValue = 1;
        var sexoViewModelValue = sexoValue == 1 ? Application.Gerencia.ViewModels.Pessoa.Enum.SexoViewModel.Masculino : 
                                 Application.Gerencia.ViewModels.Pessoa.Enum.SexoViewModel.Feminino;
        var pessoaTempViewModel = new RegistrarPessoaViewModel2
        {
            Nome = viewModel.Nome,
            Telefone = viewModel.Telefone,
            Endereco = new Application.Gerencia.ViewModels.Pessoa.EnderecoViewModel
            {
                Cep = 48300000,
                Logradouro  = "Temporario",
                Bairro = "Temporario",
                Localidade = "Temporario",
                Uf  = "SE"
            },
            DataDeNascimento = DateTime.UtcNow,
            Sexo = sexoViewModelValue
        };

        var response = _appService.RegistrarPessoa(pessoaTempViewModel);

        return Task.FromResult<IActionResult>(Ok(response));
    }
}
