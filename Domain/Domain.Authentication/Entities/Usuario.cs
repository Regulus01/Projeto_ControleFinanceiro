using Domain.Authentication.Shared;

namespace Domain.Authentication.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Slug { get; private set; }
    public Guid RoleId { get; private set; }
    public virtual Role Role { get; private set; }
    public Guid PessoaId { get; private set; }
    public virtual Pessoa Pessoa { get; private set; }

    public Usuario(string name, string email, string passwordHash, string slug, Guid roleId)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Slug = slug;
        RoleId = roleId;
    }

    public void InformePessoaId(Guid pessoaId)
    {
        PessoaId = pessoaId;
    }
}