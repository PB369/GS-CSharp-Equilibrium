using Microsoft.EntityFrameworkCore;
using Equilibrium.Api.Models;

namespace Equilibrium.Api.Data
{
    /// <summary>
    /// Contexto principal da aplicação, responsável por mapear
    /// as entidades para o banco de dados e configurar o EF Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Workspace> Workspaces { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        /// <summary>
        /// Configurações adicionais do modelo.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeds de exemplo.
            modelBuilder.Entity<Workspace>().HasData(
                new Workspace { Id = 1, Name = "Home Office", Description = "Trabalho remoto em casa" },
                new Workspace { Id = 2, Name = "Escritório Principal", Description = "Presencial - Sede" }
            );
        }
    }
}
