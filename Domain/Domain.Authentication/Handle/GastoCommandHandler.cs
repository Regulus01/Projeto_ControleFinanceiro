using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Commands.Notification;
using Domain.Authentication.Entities;
using Domain.Authentication.Entities.Enum;
using Domain.Authentication.Interface;
using MediatR;

namespace Domain.Authentication.Handle;

public class GastoCommandHandler : IRequestHandler<RegisterGastoCommand, string>, IRequestHandler<RemoverGastoCommand, string>
{
    private readonly IUsuarioRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GastoCommandHandler(IUsuarioRepository repository, IMediator mediator, IMapper mapper)
    {
        _repository = repository;
        _mediator = mediator;
        _mapper = mapper;

    }

    public async Task<string> Handle(RegisterGastoCommand request, CancellationToken cancellationToken)
    {
        var gasto = _mapper.Map<Gasto>(request);

        gasto.InformeUsuarioId(request.UsuarioId);
        gasto.InformeDataDoGasto(request.DataDoGasto ?? DateTimeOffset.UtcNow);

        if (gasto.CategoriaId == Guid.Empty || gasto.CategoriaId == null)
        {
            gasto.CategoriaId = null;
            gasto.Tipo = TipoDoGasto.Entrada;
        }

        _repository.AdicionarGasto(gasto);

        var categoria = _repository.ObterCategoriaPorId(request.CategoriaId);

        try
        {
            _repository.Commit();
            Console.WriteLine("Gasto inserido com sucesso:  " + request);
            await _mediator.Publish(new GastoCriadoNotification { Nome = gasto.Nome}, cancellationToken);
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace },
                cancellationToken);

            return "Ocorreu um erro no momento da inserção do gasto";
        }

        return "Gasto inserido com sucesso";

    }

    public async Task<string> Handle(RemoverGastoCommand request, CancellationToken cancellationToken)
    {
        var gasto = _repository.ObterGastos(x => x.Id == request.GastoId && x.UsuarioId == request.UsuarioId).FirstOrDefault();

        if (gasto == null)
            return "Gasto não encontrado";
        
        _repository.RemoverGasto(gasto);
        _repository.Commit();
        
        try
        {
            _repository.Commit();
            Console.WriteLine("Gasto removido com sucesso:  " + request);
            await _mediator.Publish(new GastoCriadoNotification { Nome = gasto.Nome}, cancellationToken);
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace },
                cancellationToken);

            return "Ocorreu um erro no momento da inserção do gasto";
        }

        return "Gasto removido com sucesso";

    }
}