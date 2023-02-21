using Application.Authentication.Interface;
using Application.Authentication.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands;
using MediatR;

namespace Application.Authentication.AppService;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsuarioAppService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<string> RegisterUser(RegisterViewModel viewmodel)
    {
        var command = _mapper.Map<RegisterUserCommand>(viewmodel);

        var result = await _mediator.Send(command);
        
        return result;
    }
}