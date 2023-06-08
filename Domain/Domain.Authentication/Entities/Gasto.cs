using Domain.Authentication.Entities.Enum;

namespace Domain.Authentication.Entities;

public class Gasto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public DateTimeOffset Data { get; private set; }
    public double Valor { get; private set; }
    public TipoDoGasto Tipo { get; private set; }
    public Guid CategoriaId { get; private set; }
    public virtual Categoria Categoria { get; private set; }
    
    public Guid UsuarioId { get; private set; }
    public virtual Usuario Usuario { get; private set; }


    public void InformeUsuarioId(Guid usuarioId)
    {
        UsuarioId = usuarioId;
    }
    
    public void InformeDataDoGasto(DateTimeOffset data)
    {
        Data = data;
    }
}