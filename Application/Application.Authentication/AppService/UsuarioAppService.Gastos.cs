using System.Linq.Expressions;
using Application.Authentication.Extensions;
using Application.Authentication.ViewModels.Gastos;
using Application.Authentication.ViewModels.Gastos.Enum;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;

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
    
    public List<GastoComCategoriaViewModel> ObterGastos(DateTimeOffset? dataInicio, DateTimeOffset? dataFim,
        bool trintaDias = false, int? pagina = 0)
    {
        Expression<Func<Gasto, bool>> predicate = x => x.UsuarioId.Equals(_user.GetUserId());

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

        var gastoComCategoriaViewModel = _mapper.Map<List<GastoComCategoriaViewModel>>(_usuarioRepository
            .ObterGastos(predicate, pagina));

        return gastoComCategoriaViewModel;
    }
}