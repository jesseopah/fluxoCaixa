using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Domain.SeedWork;

namespace Scf.Servico.Domain.Interfaces.Services
{
    public interface IConsolidadoService : IService
    {
        Task<IEnumerable<ConsolidadoResponse>?> ListarAsync();
        Task<TConsolidados> BuscarPorDataAsync(DateTime data);
        Task<bool> AtualizarPorIdAsync(int id, TConsolidados consolidado);        
        Task<bool> InserirNovoConsolidado(TConsolidados consolidado);
    }    
}
