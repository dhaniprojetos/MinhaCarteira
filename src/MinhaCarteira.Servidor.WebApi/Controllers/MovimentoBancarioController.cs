using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MinhaCarteira.Comum.Definicao.Filtro;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class MovimentoBancarioController : BaseController<MovimentoBancario>
    {
        public MovimentoBancarioController(IMovimentoBancarioServico servico)
            : base(servico) { }

        [HttpGet("obter-movimentos-para-conciliacao")]
        public async Task<IActionResult> ObterMovimentosParaConciliacao([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] FiltroBase criterio)
        {
            IActionResult resposta;
            try
            {
                criterio ??= new FiltroBase();

                var itens = await ((IMovimentoBancarioServico)Servico).ObterMovimentosParaConciliacao(criterio);

                resposta = itens == null || itens.Item2.Count == 0
                    ? NotFound(new RespostaPaginada<IList<MovimentoBancario>>(
                        null,
                        criterio.Pagina,
                        criterio.ItensPorPagina,
                        itens.Item1,
                        "Nenhum registro localizado."))
                    : Ok(new RespostaPaginada<IList<MovimentoBancario>>(
                        itens.Item2,
                        criterio.Pagina,
                        criterio.ItensPorPagina,
                        itens.Item1,
                        "Itens localizados com sucesso."));
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }
    }
}
