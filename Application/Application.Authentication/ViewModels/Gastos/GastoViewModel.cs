using Application.Authentication.ViewModels.Gastos.Enum;

namespace Application.Authentication.ViewModels.Gastos;

public class GastoViewModel
{
    public string Nome { get; set; }
    public Guid CategoriaId { get; set; }
    public double Valor { get;  set; }
    public TipoDoGastoViewModel Tipo { get; set; }

}