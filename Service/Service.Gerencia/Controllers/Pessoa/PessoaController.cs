using Application.Gerencia.Interface;
using Application.Gerencia.ViewModels.Pessoa;
using Application.Gerencia.ViewModels.Saldo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Gerencia.Controllers.Pessoa;

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
    /// EndPoint de uso do backend
    /// <remarks>
    ///     EndPoint utilizado para cadastrar a pessoa no Sistema
    /// </remarks>
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("v1/RegistrarPessoa")]
    public Task<IActionResult> RegistrarPessoa([FromBody] RegistrarPessoaViewModel viewModel)
    {
        var response = _appService.RegistrarPessoa(viewModel);

        return Task.FromResult<IActionResult>(Ok(response));
    }

}
