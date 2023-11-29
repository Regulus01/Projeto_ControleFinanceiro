using Application.Authentication.ViewModels.Gastos;
using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Entities;

namespace Application.Authentication.AutoMapper.ViewModelToCommand;

public class GastoViewModelCommandMapProfile : Profile
{
    public GastoViewModelCommandMapProfile()
    {
        CreateMap<GastoViewModel, RegisterGastoCommand>();
        CreateMap<Gasto, GastoComCategoriaViewModel>()
            .ForMember(x => x.NomeCategoria, opt => opt.MapFrom(y => y.Categoria.Nome))
            .ForMember(x => x.DataVerdadeira, opt => opt.MapFrom(y => y.Data.Date.ToString("dd-MM-yyyy")));
    }
}