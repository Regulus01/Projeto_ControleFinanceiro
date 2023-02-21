namespace Domain.Authentication.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Slug { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Usuario(string name, string email, string passwordHash, string slug, Guid roleId)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Slug = slug;
        RoleId = roleId;
    }
}