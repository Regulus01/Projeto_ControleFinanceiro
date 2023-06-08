using Application.Authentication.Interface;
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
    private readonly IUsuarioAppService _appService;

    public CategoriaController(IAuthenticatedUser user, IUsuarioAppService appService)
    {
        _user = user;
        _appService = appService;
    }

    /*
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
    */
    

    /// <summary>
    /// Endpoint utilizado para obter as categorias do usuário
    /// </summary>
    /// <remarks>
    ///     EndPoint authorize utilizado para obter as categorias do usuário
    /// </remarks>
    /// <returns></returns>
    [Route("ObterCategorias")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ObterCategorias()
    {
        var categorias = _appService.ObterCategorias();

        return Ok(categorias);
    }
    
}