using Mapster;
using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Domain.Interfaces.Repositories;
using Scf.Servico.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.ApplicationCore.Services
{
    public class LancamentoService : ILancamentoService
    {
        #region Propriedades
        private readonly ILancamentoRepository LancamentoRepository;
        private bool disposedValue;
        #endregion

        #region Construtor
        public LancamentoService(
            ILancamentoRepository LancamentoRepository)
        {
            this.LancamentoRepository = LancamentoRepository;
        }
        #endregion

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        [ExcludeFromCodeCoverageAttribute]
        ~LancamentoService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region CRUD
        public async Task<IEnumerable<LancamentoResponse>?> ListarAsync()
        {

            var lista = (await LancamentoRepository.List());

            lista = lista.OrderBy(x => x.DataDoLancamento);

            return lista.Adapt<IEnumerable<LancamentoResponse>>();
        }

        public async Task<Tlancamentos> BuscarPorIdAsync(int id)
        {

            var lista = await LancamentoRepository.List();
            var lancamento = lista.FirstOrDefault(x => x.Codigo == id);
            return lancamento;
        }

        public async Task<bool> AtualizarPorIdAsync(int id, Tlancamentos lancamento)
        {

            if (lancamento == null)
            {
                return false;
            }

            var existente = await LancamentoRepository.findById(id);

            if (existente != null)
            {
                if (existente.Codigo == lancamento.Codigo)
                {
                    existente.DataDoLancamento = lancamento.DataDoLancamento;
                    existente.DescricaoLancamento = lancamento.DescricaoLancamento;
                    existente.TipoLancamento = lancamento.TipoLancamento;
                    existente.ValorLancamento = lancamento.ValorLancamento;
                    await LancamentoRepository.Update(existente);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> ExcluirPorIdAsync(int id)
        {
            var existente = await LancamentoRepository.findById(id);

            if (existente != null)
            {
                await LancamentoRepository.Delete(existente);
                return true;

            }

            return false;
        }

        public async Task<bool> InserirNovoLancamento(Tlancamentos lancamento)
        {
            await LancamentoRepository.Add(lancamento);
            return true;
        }

        #endregion

    }
}
