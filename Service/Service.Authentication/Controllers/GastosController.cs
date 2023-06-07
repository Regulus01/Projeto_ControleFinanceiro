using Application.Authentication.Interface;
using Application.Authentication.ViewModels.Gastos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/Categoria")]
[ApiController]
public class GastosController : ControllerBase
{
    private readonly IUsuarioAppService _appService;

    public GastosController(IUsuarioAppService appService)
    {
        _appService = appService;
    }

    
    /// <summary>
    /// EndPoint utilizado para inserir gastos do usuário
    /// </summary>
    /// <returns></returns>
    [Route("InserirGasto")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> InserirGasto(GastoViewModel gastoViewModel)
    {
        var response = _appService.InserirGasto(gastoViewModel);
        return Ok(response);
    }
}