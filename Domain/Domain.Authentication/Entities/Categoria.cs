namespace Domain.Authentication.Entities;

public class Categoria
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    
    public Guid UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
    public virtual Gasto Gasto { get; private set; }
    
}