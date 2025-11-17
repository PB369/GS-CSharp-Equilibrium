using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Equilibrium.Api.Data
{
    /// <summary>
    /// Destinada a ser usada pelo Entity Framework 
    /// para criar instâncias de AppDbContext durante migrações.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// Cria uma instância de AppDbContext,
        /// carregando a configuração e a connection string do arquivo appsettings.json.
        /// </summary>
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var conn = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(conn, ServerVersion.AutoDetect(conn))
                .Options;

            return new AppDbContext(options);
        }
    }
}
