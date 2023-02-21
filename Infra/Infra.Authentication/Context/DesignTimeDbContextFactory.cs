using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.Authentication.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuthenticationContext>
{
    public AuthenticationContext CreateDbContext(string[] args)
    {
        var fileName = Directory.GetCurrentDirectory() + $"/Config/appsettings.json";

        var configuration = new ConfigurationBuilder().AddJsonFile(fileName).Build();
        var connectionString = configuration.GetConnectionString("App");

        var optionsBuilder = new DbContextOptionsBuilder<AuthenticationContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AuthenticationContext(optionsBuilder.Options);
        
    }
}