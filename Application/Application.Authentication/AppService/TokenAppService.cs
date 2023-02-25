using Application.Authentication.Interface;
using Application.Authentication.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands.Token;
using Domain.Authentication.Configuration;
using MediatR;

namespace Application.Authentication.AppService;

public class TokenAppService : ITokenAppService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TokenAppService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public Task<TokenModel?> GerarNovoToken(TokenViewModel? tokenModel)
    {
        if (tokenModel is null)
        {
            return null;
        }
        
        var command = _mapper.Map<GerarNovoTokenCommand>(tokenModel);

        var result = _mediator.Send(command);

        return result;
    }
}