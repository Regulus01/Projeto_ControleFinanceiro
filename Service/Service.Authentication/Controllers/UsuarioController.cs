using Application.Authentication.Interface;
using Application.Authentication.ViewModels;
using Infra.Authentication.Context;
using Infra.CrossCutting.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/Usuario")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioAppService _appService;
    private readonly IAuthenticatedUser _user;

    public UsuarioController(IUsuarioAppService appService, IAuthenticatedUser user)
    {
        _appService = appService;
        _user = user;
    }
    
    /// <summary>
    ///  End point utilizado para criar um usuário no sistema
    /// </summary>
    ///  <remarks>
    ///       O usuário criado no endPoint por padrão é cadastrado com a role de cliente
    ///  </remarks>
    /// <param name="viewModel"> Parametro contendo dados necessários para criação</param>
    /// <response code="200"> Usuário cadastrado </response>
    /// <response code="401"> Não autorizado </response>
    /// <response code="500"> Falha na requisição </response>
    /// <returns>Response com dados sobre o cadastro</returns>
    [AllowAnonymous]
    [HttpPost("v1/register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var response = await _appService.RegisterUser(viewModel);

        return Ok(response);
    }
    
    [HttpGet("v1/saldo")]
    public async Task<IActionResult> ObterSaldo()
    {
        var response = _appService.ObterSaldo();
            
        return Ok(response);
    }
}