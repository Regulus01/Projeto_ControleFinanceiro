using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.Gerencia.Context.Factory;

public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
{
    public T CreateDbContext(string[] args)
    {
        var fileName = Directory.GetCurrentDirectory() + $"/Config/appsettings.json";

        var configuration = new ConfigurationBuilder().AddJsonFile(fileName).Build();
        var connectionString = configuration.GetConnectionString("App");

        var optionsBuilder = new DbContextOptionsBuilder<T>();
        optionsBuilder.UseNpgsql(connectionString);
        
        return Activator.CreateInstance(typeof(T), optionsBuilder.Options) as T;
    }
}