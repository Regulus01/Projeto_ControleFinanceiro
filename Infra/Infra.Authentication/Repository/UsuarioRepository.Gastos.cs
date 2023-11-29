using System.Linq.Expressions;
using Application.Authentication.ViewModels.Gastos;
using Domain.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public partial class UsuarioRepository
{   
    public List<Gasto> ObterGastos(Expression<Func<Gasto, bool>> predicate, int? pagina = 0) 
    {
        return ObterGastosAsync(predicate, pagina).Result;
    }

    public async Task<List<Gasto>> ObterGastosAsync(Expression<Func<Gasto, bool>> predicate, int? pagina = 0)
    {
        int TamanhoPagina = 5;

        await semaphore.WaitAsync(); // Aguarde a permissão do semáforo

        try
        {
            var gastos = _context
                .Gastos
                .Where(predicate)
                .Include(x => x.Categoria)
                .AsNoTracking(); // Desabilita o rastreamento de entidades para leituras

            if (pagina > 0)
            {
                var resultadoPaginado = await gastos.OrderByDescending(x => x.Data)
                    .Skip(TamanhoPagina * (pagina.Value - 1))
                    .Take(TamanhoPagina)
                    .ToListAsync();

                return resultadoPaginado;
            }

            return await gastos.ToListAsync();
        }
        finally
        {
            semaphore.Release(); // Libera o semáforo para permitir que outras operações acessem o contexto
        }
    }
}