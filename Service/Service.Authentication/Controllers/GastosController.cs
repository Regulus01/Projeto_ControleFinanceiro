using Application.Authentication.Interface;
using Application.Authentication.ViewModels.Gastos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Authentication.Controllers;

[Authorize]
[Route("api/Categoria")]
[ApiController]
public partial class GastosController : ControllerBase
{
    private readonly IUsuarioAppService _appService;

    public GastosController(IUsuarioAppService appService)
    {
        _appService = appService;
    }
    
    /// <summary>
    /// EndPoint utilizado para inserir gastos do usuário
    /// </summary>
    /// <remarks>
    ///     EndPoint authorize utilizado para inserir gastos do usuário
    /// </remarks>
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
    /// <remarks>
    ///     EndPoint authorize utilizado para obter os gastos do usuário
    /// </remarks>
    /// <returns></returns>
    [Route("ObterGastos")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim, int? pagina = 0)
    {
        var response = _appService.ObterGastos(dataInicio, dataFim, pagina:pagina);
        return Ok(response);
    }
    
    /// <summary>
    /// EndPoint utilizado para obter gastos dos ultimos 30 dias.
    /// </summary>
    /// <remarks>
    ///     EndPoint authorize utilizado para obter os gastos do usuário
    /// </remarks>
    /// <returns></returns>
    [Route("ObterGastosTrintaDias")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ObterGastosTrintaDias()
    {
        var response = _appService.ObterGastos(null, null, true);
        return Ok(response);
    }
    
    /// <summary>
    /// EndPoint utilizado para obter gastos por categoria
    /// </summary>
    /// <remarks>
    ///     EndPoint authorize utilizado para obter os gastos do usuário logado por categoria
    /// </remarks>
    /// <returns></returns>
    [Route("ObterGastoPorCategoria")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ObterGastoPorCategoria(Guid CategoriaId)
    {
        var response = _appService.ObterGastoPorCategoria(CategoriaId);
        return Ok(response);
    }
}