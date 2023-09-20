using Application.Authentication.ViewModels;
using Application.Authentication.ViewModels.Categoria;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Entities.Enum;

namespace Application.Authentication.Interface;

public interface IUsuarioAppService
{
    Task<string> RegisterUser(RegisterViewModel viewmodel);
    Task<string> InserirGasto(GastoViewModel gastoViewModel);
    Task<string> RemoverGasto(Guid gastoId);

    double ObterSaldo();

    List<GastoComCategoriaViewModel> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim,
                                                 TipoDoGasto? tipoDoGasto, bool trintaDias = false, int? pagina = 0);
    List<GastoComCategoriaViewModel> ObterGastoPorCategoria(Guid categoriaId);
    List<CategoriaViewModel> ObterCategorias();

}