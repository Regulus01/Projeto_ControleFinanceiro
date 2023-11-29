using Application.Authentication.ViewModels.Gastos.Enum;

namespace Application.Authentication.ViewModels.Gastos;

public class GastoComCategoriaViewModel
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public DateTimeOffset Data { get; set; }
    public string DataVerdadeira { get; set; }
    public TipoDoGastoViewModel Tipo { get; set; }
    public string NomeCategoria { get; set; }
    public double Valor { get;  set; }
}