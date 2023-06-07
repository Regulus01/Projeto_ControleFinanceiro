using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Commands;

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
}