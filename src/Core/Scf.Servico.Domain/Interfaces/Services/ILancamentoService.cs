using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Domain.SeedWork;

namespace Scf.Servico.Domain.Interfaces.Services
{
    public interface ILancamentoService : IService
    {
        Task<IEnumerable<LancamentoResponse>?> ListarAsync();
        Task<Tlancamentos> BuscarPorIdAsync(int id);
        Task<bool> AtualizarPorIdAsync(int id, Tlancamentos lancamento);
        Task<bool> ExcluirPorIdAsync(int id);
        Task<bool> InserirNovoLancamento(Tlancamentos lancamento);
    }    
}
