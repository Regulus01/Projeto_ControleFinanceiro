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
    
    /// <summary>
    /// EndPoint utilizado para obter gastos do usuário
    /// </summary>
    /// <returns></returns>
    [Route("ObterGastos")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim)
    {
        var response = _appService.ObterGastos(dataInicio, dataFim);
        return Ok(response);
    }
    
    /// <summary>
    /// EndPoint utilizado para obter gastos dos ultimos 30 dias.
    /// </summary>
    /// <returns></returns>
    [Route("ObterGastosTrintaDias")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ObterGastosTrintaDias()
    {
        var response = _appService.ObterGastos(null, null, true);
        return Ok(response);
    }
}