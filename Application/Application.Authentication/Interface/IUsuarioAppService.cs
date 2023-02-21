using Application.Authentication.ViewModels;

namespace Application.Authentication.Interface;

public interface IUsuarioAppService
{
    Task<string> RegisterUser(RegisterViewModel viewmodel);
}