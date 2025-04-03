using Scf.Servico.Domain.SeedWork;
using Scf.Servico.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Scf.Servico.Infrastructure.Sql
{
    [ExcludeFromCodeCoverageAttribute]
    public abstract class CachedRepository<TEntity> : ICachedRepository<TEntity>
        where TEntity : Entity, new()
    {
        private readonly AppDbContext appDbContext;
        private readonly ICache cache;
        private bool disposedValue;

        protected CachedRepository(AppDbContext dbContext, ICache cache)
        {
            this.appDbContext = dbContext;
            this.cache = cache;
        }

        public async Task<IEnumerable<TEntity>> List(int limit = 0)
        {
            string key = $"{typeof(TEntity)}:{nameof(this.List)}";
            
            if (!this.cache.TryGet(key, out IEnumerable<TEntity> list))
            {
                var set = this.appDbContext.Set<TEntity>().AsQueryable();

                if (limit > 0)
                    set = set.Take(limit);

                list = await set.ToListAsync();

                this.cache.Set(key, list);
            }

            return list;
        }

        public async Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> query, int limit = 0)
        {   
            string key = $"{typeof(TEntity)}:{nameof(this.List)}:{query.Simplify()}";

            if (!this.cache.TryGet(key, out IEnumerable<TEntity> list))
            {
                var set = this.appDbContext.Set<TEntity>().Where(query);

                if (limit > 0)
                    set = set.Take(limit);

                list = await set.ToListAsync();

                this.cache.Set(key, list);
            }

            return list;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    appDbContext.Dispose();
                    cache.Dispose(); 
                }

                disposedValue = true;
            }
        }

        ~CachedRepository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
