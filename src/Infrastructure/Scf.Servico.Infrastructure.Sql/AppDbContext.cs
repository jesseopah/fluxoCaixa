using Microsoft.EntityFrameworkCore;
using Scf.Servico.Sql.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Sql
{
    [ExcludeFromCodeCoverageAttribute]
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //public DbSet<TConsolidado> tconsolidado { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LancamentoConfiguration());           
        }
    }
}
