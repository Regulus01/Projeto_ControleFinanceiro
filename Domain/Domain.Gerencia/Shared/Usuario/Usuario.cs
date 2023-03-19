using Domain.Gerencia.Entities;

namespace Domain.Gerencia.Shared.Usuario;

public class Usuario
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Slug { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    
    public Guid PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }
}