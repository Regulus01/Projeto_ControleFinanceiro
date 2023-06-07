using Application.Authentication.ViewModels;
using Application.Authentication.ViewModels.Categoria;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Configuration;

namespace Application.Authentication.Interface;

public interface IUsuarioAppService
{
    Task<string> RegisterUser(RegisterViewModel viewmodel);
    Task<string> InserirGasto(GastoViewModel gastoViewModel);
    List<CategoriaViewModel> ObterCategorias();
    Guid? TesteAppService();
}