using ConcorrenciaEF.Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConcorrenciaEF.Application.infra
{
    public class NegocioDbContext : DbContext
    {
        private readonly string _connectionString;

        public NegocioDbContext(DbContextOptions<NegocioDbContext> options)
        : base(options)
        {
        }

        public NegocioDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Negocio> Negocios { get; set; }
    }
}