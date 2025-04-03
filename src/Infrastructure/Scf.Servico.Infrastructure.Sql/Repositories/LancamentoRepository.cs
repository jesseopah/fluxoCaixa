using Scf.Servico.Domain.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Sql.Repositories
{
    /// <summary>
    /// Classe LancamentoRepository
    /// </summary>
    [ExcludeFromCodeCoverageAttribute]
    public class LancamentoRepository : Repository<Domain.Entities.Tlancamentos>, ILancamentoRepository
    {
        public LancamentoRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
