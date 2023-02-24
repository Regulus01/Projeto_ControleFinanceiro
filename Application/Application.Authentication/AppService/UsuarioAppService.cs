using Application.Authentication.Interface;
using Application.Authentication.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands;
using Infra.CrossCutting.Interface;
using MediatR;

namespace Application.Authentication.AppService;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IAuthenticatedUser _user;

    public UsuarioAppService(IMapper mapper, IMediator mediator, IAuthenticatedUser user)
    {
        _mapper = mapper;
        _mediator = mediator;
        _user = user;
    }
    public async Task<string> RegisterUser(RegisterViewModel viewmodel)
    {
        var command = _mapper.Map<RegisterUserCommand>(viewmodel);

        var result = await _mediator.Send(command);
        
        return result;
    }

    public Guid? TesteAppService()
    {
        var x = _user.GetUserId();
        Console.WriteLine(x);

        return x;
    }
}