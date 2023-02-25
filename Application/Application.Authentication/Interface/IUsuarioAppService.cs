using Application.Authentication.ViewModels;
using Domain.Authentication.Configuration;

namespace Application.Authentication.Interface;

public interface IUsuarioAppService
{
    Task<string> RegisterUser(RegisterViewModel viewmodel);
    Guid? TesteAppService();
}