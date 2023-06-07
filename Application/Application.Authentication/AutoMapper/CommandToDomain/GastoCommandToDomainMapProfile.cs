using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;

namespace Application.Authentication.AutoMapper.CommandToDomain;

public class GastoCommandToDomainMapProfile : Profile
{
    public GastoCommandToDomainMapProfile()
    {
        CreateMap<RegisterGastoCommand, Gasto>();
    }
}