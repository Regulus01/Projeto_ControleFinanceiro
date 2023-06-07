using Application.Authentication.ViewModels.Gastos;
using AutoMapper;
using Domain.Authentication.Commands;

namespace Application.Authentication.AutoMapper.ViewModelToCommand;

public class GastoViewModelCommandMapProfile : Profile
{
    public GastoViewModelCommandMapProfile()
    {
        CreateMap<GastoViewModel, RegisterGastoCommand>();
    }
}