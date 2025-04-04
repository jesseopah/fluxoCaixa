using Microsoft.EntityFrameworkCore;
using Scf.Servico.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Scf.Servico.Infrastructure.Sql
{
    [ExcludeFromCodeCoverageAttribute]
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity, new()
    {
        private readonly AppDbContext appDbContext;
        private bool disposedValue;

        protected Repository(AppDbContext dbContext) =>
            this.appDbContext = dbContext;

        public async Task<IEnumerable<TEntity>> List(int limit=0) 
        {
            var set = appDbContext.Set<TEntity>().AsQueryable();

            if (limit > 0)
                set = set.Take(limit);

            return await set.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> query, int limit = 0) 
        {
            var set = appDbContext.Set<TEntity>().Where(query);

            if (limit > 0)
                set = set.Take(limit);

            return await set.ToListAsync();
        }

        public async Task<TEntity> Add(TEntity entity) 
        {
            var item = await appDbContext.Set<TEntity>().AddAsync(entity);
            await appDbContext.SaveChangesAsync();

            return item.Entity;
        }

        public async Task Update(TEntity entity) 
        {
            appDbContext.Entry(entity).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            appDbContext.Set<TEntity>().Remove(entity);
            await appDbContext.SaveChangesAsync();
        }
             
        public async Task<TEntity> findById(int id)
        {
            return await appDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Codigo == id);
        }
              

        public async Task Load()
        {
            await appDbContext.Set<TEntity>().LoadAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.appDbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        ~Repository()
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
