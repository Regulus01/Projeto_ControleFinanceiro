using System.Linq.Expressions;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository
{
    public List<Gasto> ObterGastos(Expression<Func<Gasto, bool>> predicate)
    {
        var gastos = _context.Gastos.Where(predicate).Include(x => x.Categoria).ToList();

        return gastos;
    }
}