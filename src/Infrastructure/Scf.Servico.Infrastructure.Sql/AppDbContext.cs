using Microsoft.EntityFrameworkCore;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Sql.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Sql
{
    [ExcludeFromCodeCoverageAttribute]
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<TConsolidados> tconsolidados { get; set; }
        public DbSet<Tlancamentos> tlancamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConsolidadoConfiguration());
            modelBuilder.ApplyConfiguration(new LancamentoConfiguration());
        }
    }
}
