using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.WebApi.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TEntidade> : ControllerBase
    {
        protected IServicoCrud<TEntidade> Servico { get; }

        public BaseController(IServicoCrud<TEntidade> servico)
        {
            Servico = servico;
        }

        //[HttpGet]
        //public async Task<IList<TEntidade>> Navegar()
        //{
        //    var itens = await Servico.Navegar(null);
        //    
        //    return itens;
        //}
        
        [HttpGet]
        public async Task<IActionResult> Navegar()
        {
            var itens = await Servico.Navegar(null);

            return itens == null 
                ? NotFound() 
                : Ok(new Resposta<IList<TEntidade>>(itens));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var itemDb = await Servico.ObterPorId(id);

            return itemDb != null
                ? Ok(new Resposta<TEntidade>(itemDb))
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Incluir(IList<TEntidade> itens)
        {
            var itemDb = await Servico.Incluir(itens);

            return itemDb != null
                ? CreatedAtAction(
                    nameof(Incluir), 
                    new Resposta<IList<TEntidade>>(itemDb))
                : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(IList<TEntidade> itens)
        {
            var itemDb = await Servico.Alterar(itens);

            return itemDb != null
                ? Ok(new Resposta<IList<TEntidade>>(itemDb))
                : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int id)
        {
            var linhasAfetadas = await Servico.Deletar(new[] { id });

            return linhasAfetadas > 0
                ? Ok(Ok(new Resposta<int>(linhasAfetadas)))
                : NotFound();
        }

    }
}
