using Scf.Servico.Domain.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Sql.Repositories
{
    /// <summary>
    /// Classe ConsolidadoRepository
    /// </summary>
    [ExcludeFromCodeCoverageAttribute]
    public class ConsolidadoRepository : Repository<Domain.Entities.TConsolidados>, IConsolidadoRepository
    {
        public ConsolidadoRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
