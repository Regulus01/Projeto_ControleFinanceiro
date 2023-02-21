using SecureIdentity.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Authentication.ViewModels;
using Domain.Authentication.Configuration;
using Domain.Authentication.Entities;
using Infra.Authentication.Context;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("v1/register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model,
        [FromServices] AuthenticationContext context)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new Usuario
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-"),
            RoleId = Guid.Parse("fc1eb138-1c84-4fb0-846b-0d8f45d6aac3"),
            // PasswordHasher.Hash pertence ao pacote SecureIdentity,
            // e irá criptografar a senha do usuário
            PasswordHash = PasswordHasher.Hash(model.Password),
          
        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok($"{user.Email} {user.PasswordHash}");
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, "Duplicate Email");
        }
        catch
        {
            return StatusCode(500, "Internal Error");
        }
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
    public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

    [HttpGet]
    [Route("cliente")]
    [Authorize(Roles = "cliente")]
    public string Employee() => "admin";

}