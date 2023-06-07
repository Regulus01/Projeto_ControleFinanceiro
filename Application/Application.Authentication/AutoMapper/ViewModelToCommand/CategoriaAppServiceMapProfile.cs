using Application.Authentication.ViewModels.Categoria;
using AutoMapper;
using Domain.Authentication.Entities;

namespace Application.Authentication.AutoMapper.ViewModelToCommand;

public class CategoriaAppServiceMapProfile : Profile
{
    public CategoriaAppServiceMapProfile()
    {
        CreateMap<Categoria, CategoriaViewModel>();
    }
}