using Microsoft.AspNetCore.Mvc;
using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Scf.Servico.Domain.Interfaces.Services;

namespace Scf.Servico.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LancamentoController : ControllerBase
    {

        #region Propriedades
        private readonly ILancamentoService LancamentoService;
        #endregion

        #region Construtor
        public LancamentoController(ILancamentoService LancamentoService)
        {
            this.LancamentoService = LancamentoService;
        }
        #endregion

        #region HTTP

        //GET api/<Lancamento>
        [HttpGet]       
        public async Task<IEnumerable<LancamentoResponse>?> Get()
        {
            return await LancamentoService.ListarAsync();
        }

        //GET api/<Lancamento>/5
        [HttpGet("{id}")]
        public async Task<Tlancamentos> Get(int id)
        { 
           return await LancamentoService.BuscarPorIdAsync(id);           
        }

        //POST api/<Lancamento>
        [HttpPost]
        public Task<bool> Post(Tlancamentos lancamento)
        {
            return LancamentoService.InserirNovoLancamento(lancamento);           
        }

        //PUT api/<Lancamento>/5
        [HttpPut("{id}")]
        public Task<bool> Put(int id, [FromBody] Tlancamentos lancamento)
        {         
            return LancamentoService.AtualizarPorIdAsync(id, lancamento);
        }

        // DELETE api/<Lancamento>/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(int id)
        {
            return LancamentoService.ExcluirPorIdAsync(id);
        }

        #endregion

    }
}
