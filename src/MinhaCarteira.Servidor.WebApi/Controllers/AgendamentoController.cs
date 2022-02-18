using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class AgendamentoController : BaseController<Agendamento>
    {
        public AgendamentoController(IAgendamentoServico servico) : base(servico)
        {
        }

        [Route("contas-a-vencer/{qtdDias:int}")]
        [HttpGet]
        public async Task<IActionResult> ContasAVencer(int qtdDias)
        {
            IActionResult resposta;
            try
            {
                var itens = await ((IAgendamentoServico)Servico).ContasAVencer(qtdDias);
                resposta = itens == null || itens.Count == 0
                    ? NotFound(new Resposta<IList<AgendamentoItem>>(
                        null,
                        "Nenhum registro localizado."))
                    : Ok(new Resposta<IList<AgendamentoItem>>(
                        itens,
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
