using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class AgendamentoController : BaseController<Agendamento>
    {
        public AgendamentoController(IAgendamentoServico servico) : base(servico)
        {
        }

        [Route("contas-a-vencer")]
        [HttpGet]
        public async Task<IActionResult> ContasAVencer([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] ICriterio criterio)
        {
            IActionResult resposta;
            try
            {
                criterio ??= new FiltroBase();
                var itens = await ((IAgendamentoServico)Servico).ContasAVencer(criterio);
                resposta = itens == null || itens.Item2.Count == 0
                    ? NotFound(new RespostaPaginada<IList<AgendamentoItem>>(
                        null,
                        criterio.Pagina,
                        criterio.ItensPorPagina,
                        itens.Item1,
                        "Nenhum registro localizado."))
                    : Ok(new RespostaPaginada<IList<AgendamentoItem>>(
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

        [Route("obter-parcela/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> ObterParcelaPorId(int id)
        {
            IActionResult resposta;
            try
            {
                var itens = await ((IAgendamentoServico)Servico).ObterParcelaPorId(id);
                resposta = itens == null
                    ? NotFound(new Resposta<AgendamentoItem>(
                        null,
                        "Nenhum registro localizado."))
                    : Ok(new Resposta<AgendamentoItem>(
                        itens,
                        "Parcela localizada com sucesso."));
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [Route("baixar-parcela")]
        [HttpPost]
        public async Task<IActionResult> BaixarParcela([FromBody] AgendamentoItem parcela)
        {
            IActionResult resposta;
            try
            {
                var itens = await ((IAgendamentoServico)Servico).BaixarParcela(parcela);
                resposta = itens == null
                    ? NotFound(new Resposta<AgendamentoItem>(
                        null,
                        "Nenhum registro localizado."))
                    : Ok(new Resposta<AgendamentoItem>(
                        itens,
                        "Pagamento realizado com sucesso."));
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [Route("conciliar-parcela")]
        [HttpPost]
        public async Task<IActionResult> ConciliarParcela(int id, string idMovimentos)
        {
            IActionResult resposta;
            try
            {
                var conciliado = await ((IAgendamentoServico)Servico).ConciliarParcela(id, idMovimentos);
                resposta = !conciliado
                    ? NotFound(new Resposta<bool>(
                        conciliado,
                        "Não foi possível realizar a conciliação corretamente."))
                    : Ok(new Resposta<bool>(
                        conciliado,
                        "Parcela conciliada com sucesso."));
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
