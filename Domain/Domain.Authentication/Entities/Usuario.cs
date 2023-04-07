using Domain.Authentication.Shared;

namespace Domain.Authentication.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Slug { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
    public Guid PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }

    public Usuario(string name, string email, string passwordHash, string slug, Guid roleId)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Slug = slug;
        RoleId = roleId;
    }
}