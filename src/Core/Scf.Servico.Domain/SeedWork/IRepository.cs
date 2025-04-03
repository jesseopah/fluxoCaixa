using Scf.Servico.Domain.Entities;
using System.Linq.Expressions;

namespace Scf.Servico.Domain.SeedWork
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity, new()
    {
        Task<IEnumerable<TEntity>> List(int limit = 0);
        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> query, int limit = 0);
        Task<TEntity> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Load();
        Task<TEntity> findById(int id);


    }

}
