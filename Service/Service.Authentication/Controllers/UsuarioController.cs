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
    
    [Authorize(Roles = "admin")]
    [HttpPatch("v1/changeUserRole")]
    public async Task<IActionResult> ChangeRole(Guid UserId,
        Guid NewRoleId,
        [FromServices] AuthenticationContext context)
    {
        var user = await context
            .Users
            .FirstOrDefaultAsync(x => x.Id == UserId);

        var role = await context
            .Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == NewRoleId);

        if (user == null || role == null)
            return StatusCode(401, "Invalid Id");

        user.Role = role;

        await context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    /// <summary>
    ///     EndPoint utilizado para testes
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated()
    {
        //operacoes para testes 
        var x = 1;
        var y = 3;
        var idDoUsuarioLogado = _user.GetUserId();
        var soma = x + y;
        _appService.TesteAppService();
        return String.Format("Autenticado - {0}", User.Identity.Name);
    }

    [HttpGet]
    [Route("cliente")]
    [Authorize(Roles = "cliente")]
    public string Employee() => "cliente";
}