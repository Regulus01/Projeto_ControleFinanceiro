using Application.Authentication.ViewModels.Categoria;
using Infra.CrossCutting.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/Categoria")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly IAuthenticatedUser _user;

    public CategoriaController(IAuthenticatedUser user)
    {
        _user = user;
    }

    /// <summary>
    /// Endpoint utilizado para inserir uma nova categoria para o usuário
    /// </summary>
    /// <returns></returns>
    [Route("CriarCategoria")]
    [HttpPost]
    [Authorize]
    public Task<IActionResult> CriarCategoria(List<CategoriaViewModel> categorias)
    {
        
    }
}