using Comp2001.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

// This class provides a factory for creating instances of ApplicationDbContext
// during design time, such as when running Entity Framework Core migrations.
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    // This method is called to create a new instance of the ApplicationDbContext.
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Configuring the builder to read from the 'appsettings.json' file.
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Building the options for the DbContext using SQL Server.
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        // Creating and returning a new instance of ApplicationDbContext with the configured options.
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
