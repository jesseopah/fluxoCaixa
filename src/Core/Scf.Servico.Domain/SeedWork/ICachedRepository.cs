using System.Linq.Expressions;

namespace Scf.Servico.Domain.SeedWork
{
    public interface ICachedRepository<TEntity> : IDisposable
        where TEntity : Entity, new()
    {
        Task<IEnumerable<TEntity>> List(int limit = 0);
        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> query, int limit = 0);
    }
}
