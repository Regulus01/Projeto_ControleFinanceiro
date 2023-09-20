using Application.Authentication.Interface;
using Application.Authentication.ViewModels;
using Application.Authentication.ViewModels.Gastos;
using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities.Enum;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Interface;
using MediatR;

namespace Application.Authentication.AppService;

public partial class UsuarioAppService : IUsuarioAppService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IAuthenticatedUser _user;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioAppService(IMapper mapper, IMediator mediator, IAuthenticatedUser user, IUsuarioRepository usuarioRepository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _user = user;
        _usuarioRepository = usuarioRepository;
    }
    public async Task<string> RegisterUser(RegisterViewModel viewmodel)
    {
        var command = _mapper.Map<RegisterUserCommand>(viewmodel);

        var result = await _mediator.Send(command);
        
        return result;
    }

    public double ObterSaldo()
    {
        var entradas = _usuarioRepository.ObterGastos(x => x.Tipo == TipoDoGasto.Entrada && x.UsuarioId == _user.GetUserId()).Sum(x => x.Valor);
        var saidas = _usuarioRepository.ObterGastos(x => x.Tipo == TipoDoGasto.Saida && x.UsuarioId == _user.GetUserId()).Sum(x => x.Valor);

        return entradas - saidas;
    }
}