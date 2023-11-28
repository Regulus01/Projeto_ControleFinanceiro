using System.Globalization;
using System.Linq.Expressions;
using Application.Authentication.Extensions;
using Application.Authentication.ViewModels.Gastos;
using Application.Authentication.ViewModels.Gastos.Enum;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;
using Domain.Authentication.Entities.Enum;

namespace Application.Authentication.AppService;

public partial class UsuarioAppService
{
    public async Task<string> InserirGasto(GastoViewModel gastoViewModel)
    {
        if (gastoViewModel.CategoriaId == Guid.Empty || gastoViewModel.CategoriaId == null)
        {
            gastoViewModel.CategoriaId = null;
            gastoViewModel.Tipo = TipoDoGastoViewModel.Entrada;
        }

        var command = _mapper.Map<RegisterGastoCommand>(gastoViewModel);

        var usuarioId = _user.GetUserId();
        
        if (usuarioId == null)
        {
            return "Falha ao inserir gasto";
        }

        command.UsuarioId = usuarioId.Value;
        
        var result = await _mediator.Send(command);

        return result;

    }

    public async Task<string> RemoverGasto(Guid gastoId)
    {
        var usuarioId = _user.GetUserId();

        if (usuarioId != null)
        {
            var removerGastoCommand = new RemoverGastoCommand(gastoId, usuarioId.Value);
            var command = _mapper.Map<RemoverGastoCommand>(removerGastoCommand);
        
            var result = await _mediator.Send(command);

            return result;
        }

        return "Usuario nao encontrado";
    }

    public List<GastoComCategoriaViewModel> ObterGastoPorCategoria(Guid categoriaId)
    {
        if (categoriaId == Guid.Empty)
        {
            return new List<GastoComCategoriaViewModel>();
        }

        var result = _usuarioRepository.ObterGastos(x => x.CategoriaId.Equals(categoriaId) && 
                                                              x.UsuarioId.Equals(_user.GetUserId()));

        var gastosComCategoria = _mapper.Map<List<GastoComCategoriaViewModel>>(result);

        return gastosComCategoria;
    }

    public Dictionary<string, double> ObterGastosDoAno(int ano, TipoDoGasto tipo = TipoDoGasto.Entrada)
    {
     
        var result = _usuarioRepository.ObterGastos(x => x.Data.Year.Equals(ano) && 
                                                              x.UsuarioId.Equals(_user.GetUserId()) && x.Tipo == tipo)
                                                              .OrderBy(x => x.Data).ToList();

        var gastosDoAno = new Dictionary<string, double>();
        
        // Inicializar o dicionário com todos os meses do ano
        for (int i = 1; i <= 12; i++)
        {
            var nomeMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
            gastosDoAno[nomeMes] = 0.0;
        }
        
        foreach (var gasto in result)
        {
            var mes = gasto.Data.ToString("MMMM");
            if (gastosDoAno.ContainsKey(mes))
            {
                gastosDoAno[mes] += gasto.Valor;
            }
            else
            {
                gastosDoAno[mes] = gasto.Valor;
            }
        }

        return gastosDoAno;
    }

    public List<GastoComCategoriaViewModel> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim,
        TipoDoGasto? tipoDoGasto = null, bool trintaDias = false, int? pagina = 0)
    {
        Expression<Func<Gasto, bool>> predicate = x => x.UsuarioId.Equals(_user.GetUserId());

        if (dataInicio.HasValue)
            predicate = predicate.And(x => x.Data >= dataInicio);

        if (dataFim.HasValue)
            predicate = predicate.And(x => x.Data <= dataFim);

        if (tipoDoGasto != null)
            predicate = predicate.And(x => x.Tipo == tipoDoGasto);

        if (trintaDias)
        {
            dataInicio = DateTimeOffset.UtcNow.AddDays(-30);
            dataFim = DateTimeOffset.UtcNow;
            var gastoTrintaDias = ObterGastos(dataInicio, dataFim, tipoDoGasto);
            return gastoTrintaDias;
        }

        var gastoComCategoriaViewModel = _mapper.Map<List<GastoComCategoriaViewModel>>(_usuarioRepository
            .ObterGastos(predicate, pagina));

        return gastoComCategoriaViewModel;
    }
}