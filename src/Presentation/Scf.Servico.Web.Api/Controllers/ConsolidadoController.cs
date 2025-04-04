using Microsoft.AspNetCore.Mvc;
using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Domain.Interfaces.Services;

namespace Scf.Servico.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConsolidadoController : ControllerBase
    {

        #region Propriedades
        private readonly IConsolidadoService ConsolidadoService;
        #endregion

        #region Construtor
        public ConsolidadoController(IConsolidadoService ConsolidadoService)
        {
            this.ConsolidadoService = ConsolidadoService;
        }
        #endregion

        #region HTTP

        //GET api/<Consolidado>
        [HttpGet]       
        public async Task<IEnumerable<ConsolidadoResponse>?> Get()
        {
            return await ConsolidadoService.ListarAsync();
        }

        //GET api/<Consolidado>/04/04/2025
        [HttpGet("{dtconsolidado}")]
        public async Task<TConsolidados> Get(string dtconsolidado)
        {
            DateTime enteredDate = DateTime.Parse(dtconsolidado);
            return await ConsolidadoService.BuscarPorDataAsync(enteredDate);           
        }

        //POST api/<Consolidado>
        [HttpPost]
        public Task<bool> Post(TConsolidadosString consolidado)
        {
            TConsolidados request = new TConsolidados();
            request.Codigo = consolidado.Codigo;
            request.TotalEntrada = consolidado.TotalEntrada;
            request.TotalSaida = consolidado.TotalSaida;
            request.DataConsolidado = DateTime.Parse(consolidado.DataConsolidado);
            request.ValorConsolidado = consolidado.ValorConsolidado;
            return ConsolidadoService.InserirNovoConsolidado(request);           
        }

        //PUT api/<Consolidado>/5
        [HttpPut("{id}")]
        public Task<bool> Put(int id, [FromBody] TConsolidadosString consolidado)
        {
            TConsolidados request = new TConsolidados();
            request.Codigo = consolidado.Codigo;
            request.TotalEntrada = consolidado.TotalEntrada;
            request.TotalSaida = consolidado.TotalSaida;
            request.DataConsolidado = DateTime.Parse(consolidado.DataConsolidado);
            request.ValorConsolidado = consolidado.ValorConsolidado;
            return ConsolidadoService.AtualizarPorIdAsync(id,request);
        }        

        #endregion

    }
}
