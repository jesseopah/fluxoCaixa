namespace Scf.Servico.Domain.SeedWork
{
    public interface ICache : IDisposable
    {
        bool TryGet<T>(string key, out T value);
        T Set<T>(string key, T value);
        void Remove(string key);
    }
}
