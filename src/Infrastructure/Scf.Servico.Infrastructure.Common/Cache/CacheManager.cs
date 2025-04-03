using Scf.Servico.Domain.Interfaces.Services;
using Scf.Servico.Infrastructure.Common.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Scf.Servico.Domain.SeedWork;

namespace Scf.Servico.Infrastructure.Common.Cache
{
    [ExcludeFromCodeCoverageAttribute]
    public class CacheManager : ICache
    {
        private readonly IMemoryCache memoryCache;
        
        private readonly MemoryCacheEntryOptions? memoryCacheEntryOptions;
        private bool disposedValue;

        public CacheManager(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheConfig)
        {
            CacheConfiguration? cacheConfiguration;

            this.memoryCache = memoryCache;
            cacheConfiguration = cacheConfig.Value;

            if (cacheConfiguration != null)
            {
                this.memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.High,
                    AbsoluteExpiration = DateTime.Now.AddHours(cacheConfiguration.AbsoluteExpiration),
                    SlidingExpiration = TimeSpan.FromMinutes(cacheConfiguration.SlidingExpiration)
                };
            }
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = memoryCache.Get<T>(key);

            if (value != null)
            {
                Debug.WriteLine($"Consultou cache: KEY: {key} VALUE: {value}");

                return true;
            }

            Debug.WriteLine($"Consultou cache: KEY: {key} VALUE: N/E");

            return false;
        }

        public T Set<T>(string key, T value)
        {
            var item = memoryCache.Set(key, value, memoryCacheEntryOptions);

            Debug.WriteLine($"Setou cache: KEY: {key} VALUE: {item}");

            return item;
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);

            Debug.WriteLine($"Removeu cache: KEY: {key}");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        ~CacheManager()
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
