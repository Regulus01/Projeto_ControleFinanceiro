using Application.Authentication.Interface;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Entities.Enum;
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

    [Route("RemoverGasto")]
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> RemoverGasto(Guid GastoId)
    {
        var response = _appService.RemoverGasto(GastoId);

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
    public async Task<IActionResult> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim, TipoDoGasto? tipo, int? pagina = 0)
    {
        var response = _appService.ObterGastos(dataInicio, dataFim, tipo, pagina:pagina);
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
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ObterGastosTrintaDias()
    {
        var response = _appService.ObterGastosTrintaDias();
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
    
    /// <summary>
    /// EndPoint utilizado para obter gastos do ano, com filtro de tipo do gasto.
    /// </summary>
    /// <remarks>
    ///     EndPoint authorize utilizado para obter os gastos do ano, TipoGasto 1 = entrada TipoDoGasto 2 = saida
    /// </remarks>
    /// <returns></returns>
    [Route("ObterGastosDoAno")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ObterGastosDoAno([FromQuery] int ano, TipoDoGasto tipo = TipoDoGasto.Entrada)
    {
        var response = _appService.ObterGastosDoAno(ano, tipo);
        return Ok(response);
    }
}