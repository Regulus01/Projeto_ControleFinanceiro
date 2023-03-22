using Application.Gerencia.Interface;
using Application.Gerencia.ViewModels.Pessoa;
using Infra.CrossCutting.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Gerencia.Controllers.Pessoa;

[Authorize]
[Route("api/Usuario")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IPessoaAppService _appService;

    public PessoaController(IPessoaAppService appService)
    {
        _appService = appService;
    }
    
    [AllowAnonymous]
    [HttpPost("v1/register")]
    public Task<IActionResult> Register([FromBody] RegistrarPessoaViewModel viewModel)
    {
        var response = _appService.RegistrarPessoa(viewModel);

        return Task.FromResult<IActionResult>(Ok(response));
    }
}
