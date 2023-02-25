using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email precisa ser valido")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password é obrigatório")]
    public string? Password { get; set; }
}