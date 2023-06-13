using System.Linq.Expressions;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository
{
    public List<Gasto> ObterGastos(Expression<Func<Gasto, bool>> predicate, int? pagina = 0)
    {
        int TamanhoPagina = 5;

        var gastos = _context
            .Gastos.Where(predicate)
            .Include(x => x.Categoria);

        if (pagina > 0)
        {
            var teste =  gastos.OrderByDescending(x => x.Data).Skip(TamanhoPagina * (pagina.Value - 1))
                         .Take(TamanhoPagina)
                         .ToList();

            return teste;
        }


        return gastos.ToList();
    }
}