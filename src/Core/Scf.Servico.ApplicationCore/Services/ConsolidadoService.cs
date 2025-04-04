using Mapster;
using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Domain.Interfaces.Repositories;
using Scf.Servico.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.ApplicationCore.Services
{
    public class ConsolidadoService : IConsolidadoService
    {
        #region Propriedades
        private readonly IConsolidadoRepository ConsolidadoRepository;
        private bool disposedValue;
        #endregion

        #region Construtor
        public ConsolidadoService(
            IConsolidadoRepository ConsolidadoRepository)
        {
            this.ConsolidadoRepository = ConsolidadoRepository;
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
        ~ConsolidadoService()
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
        public async Task<IEnumerable<ConsolidadoResponse>?> ListarAsync()
        {

            var lista = (await ConsolidadoRepository.List());

            lista = lista.OrderBy(x => x.DataConsolidado);

            return lista.Adapt<IEnumerable<ConsolidadoResponse>>();
        }

        public async Task<TConsolidados> BuscarPorDataAsync(DateTime data)
        {

            var lista = await ConsolidadoRepository.List();
            var consolidado = lista.FirstOrDefault(x => x.DataConsolidado == data);
            return consolidado;
        }

        public async Task<bool> AtualizarPorIdAsync(int id, TConsolidados consolidado)
        {

            if (consolidado == null)
            {
                return false;
            }

            var existente = await ConsolidadoRepository.findById(id);

            if (existente != null)
            {
                if (existente.Codigo == consolidado.Codigo)
                {
                    existente.DataConsolidado = consolidado.DataConsolidado;
                    existente.TotalEntrada = consolidado.TotalEntrada;
                    existente.TotalSaida = consolidado.TotalSaida;
                    existente.ValorConsolidado = consolidado.ValorConsolidado;
                   
                    await ConsolidadoRepository.Update(existente);
                    return true;
                }
            }

            return false;
        }
        
        public async Task<bool> InserirNovoConsolidado(TConsolidados consolidado)
        {
            await ConsolidadoRepository.Add(consolidado);
            return true;
        }

        #endregion

    }
}
