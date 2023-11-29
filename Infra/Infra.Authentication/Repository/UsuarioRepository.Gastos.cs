using System.Linq.Expressions;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository
{   
    public List<Gasto> ObterGastos(Expression<Func<Gasto, bool>> predicate, int? pagina = 0)
    {
        var query = QuerySemaphore(predicate, true, x => x.Categoria).Result;

        if (!(pagina > 0))
            return query.ToList();
        
        var tamanhoPagina = 5;
        var resultadoPaginado = query.OrderByDescending(x => x.Data)
            .Skip(tamanhoPagina * (pagina.Value - 1))
            .Take(tamanhoPagina);

        return resultadoPaginado.ToList();

    }
}