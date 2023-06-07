using System.Linq.Expressions;
using Application.Authentication.Extensions;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;

namespace Application.Authentication.AppService;

public partial class UsuarioAppService
{
    public async Task<string> InserirGasto(GastoViewModel gastoViewModel)
    {
        if (gastoViewModel.CategoriaId == Guid.Empty)
        {
            return "É necessário informar uma categoria.";
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

    public List<GastoComCategoriaViewModel> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim,
        bool trintaDias = false)
    {
        Expression<Func<Gasto, bool>> predicate = x => true;

        if (dataInicio.HasValue)
            predicate = predicate.And(x => x.Data >= dataInicio);

        if (dataFim.HasValue)
            predicate = predicate.And(x => x.Data <= dataFim);

        if (trintaDias)
        {
            dataInicio = DateTimeOffset.UtcNow.AddDays(-30);
            dataFim = DateTimeOffset.UtcNow;
            var gastoTrintaDias = ObterGastos(dataInicio, dataFim);
            return gastoTrintaDias;
        }

        var gastoComCategoriaViewModel = _mapper.Map<List<GastoComCategoriaViewModel>>(_usuarioRepository.ObterGastos(predicate));

        return gastoComCategoriaViewModel;
    }
}