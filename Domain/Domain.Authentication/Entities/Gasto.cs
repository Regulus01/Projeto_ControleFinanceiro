namespace Domain.Authentication.Entities;

public class Gasto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public DateTimeOffset Data { get; private set; }
    public Guid CategoriaId { get; private set; }
    public virtual Categoria Categoria { get; private set; }
    
    public Guid UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
}