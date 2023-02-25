using Application.Authentication.Interface;
using SecureIdentity.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Authentication.ViewModels;
using Domain.Authentication.Configuration;
using Infra.Authentication.Context;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenAppService _appService;

    public AuthenticationController(ITokenAppService appService)
    {
        _appService = appService;
    }

    /// <summary>
    ///  End point utilizado para fazer login do usuário
    /// </summary>
    ///  <remarks>
    ///       Utilizado para obter o token de autenticação pra ser usado no sistema.
    ///  </remarks>
    /// <param name="LoginViewModel"> ViewModel com dados necessário para o login</param>
    /// <response code="200"> Logado com sucesso </response>
    /// <response code="500"> Falha na requisição </response>
    /// <returns>Token de autorização</returns>
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
            Console.WriteLine();
            return Ok(token);
        }
        catch
        {
            return StatusCode(500, "Internal Error");
        }
    }

    /// <summary>
    ///  End point utilizado para gerar um novo token de autenticacao
    /// </summary>
    ///  <remarks>
    ///       Utilizado para gerar um novo token a partir de um token expirado com um refresh token valido
    ///  </remarks>
    /// <param name="LoginViewModel"> ViewModel com dados necessários do token</param>
    /// <response code="200"> Novo token gerado</response>
    /// <response code="500"> Falha na requisição </response>
    /// <returns>Token de autorização</returns>
    [HttpPost]
    [Route("refreshToken")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(TokenViewModel? tokenModel)
    {
        var response = _appService.GerarNovoToken(tokenModel);
        return Ok(response);
    }



}