using Application.Authentication.ViewModels;
using Application.Authentication.ViewModels.Categoria;
using Application.Authentication.ViewModels.Gastos;
using Application.Gerencia.ViewModels.Saldo;

namespace Application.Authentication.Interface;

public interface IUsuarioAppService
{
    Task<string> RegisterUser(RegisterViewModel viewmodel);
    Task<string> InserirGasto(GastoViewModel gastoViewModel);
    List<GastoComCategoriaViewModel> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim,
                                                 bool trintaDias = false, int? pagina = 0);
    List<GastoComCategoriaViewModel> ObterGastoPorCategoria(Guid categoriaId);
    List<CategoriaViewModel> ObterCategorias();

}