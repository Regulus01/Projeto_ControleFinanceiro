using Application.Authentication.Interface;
using SecureIdentity.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Authentication.ViewModels;
using Domain.Authentication.Configuration;
using Infra.Authentication.Context;
using Infra.CrossCutting.User.Athenticated;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUsuarioAppService _appService;
    private readonly AuthenticatedUser _user;

    public AuthenticationController(IUsuarioAppService appService, AuthenticatedUser user)
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
    /// <returns>Reponse com dados sobre o cadastro</returns>
    [AllowAnonymous]
    [HttpPost("v1/register")]
    public Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid) 
            return Task.FromResult<IActionResult>(BadRequest(ModelState));

        var response = _appService.RegisterUser(viewModel);

        return Task.FromResult<IActionResult>(Ok(response));
    }
    
    [AllowAnonymous]
    [HttpPost("v1/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] AuthenticationContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.Values);

        var user = await context
            .Users
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            return StatusCode(401, "User or password invalid");

        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, "User or password invalid");

        try
        {
            var token = tokenService.GenerateToken(user);
            return Ok(token);
        }
        catch
        {
            return StatusCode(500, "Internal Error");
        }
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
        
        return String.Format("Autenticado - {0}", User.Identity.Name);
    }

    [HttpGet]
    [Route("cliente")]
    [Authorize(Roles = "cliente")]
    public string Employee() => "cliente";

}