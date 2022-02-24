using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class MovimentoBancarioController : BaseController<MovimentoBancario>
    {
        public MovimentoBancarioController(IMovimentoBancarioServico servico)
            : base(servico) { }

        [HttpGet("obter-movimentos-para-conciliacao")]
        public async Task<IActionResult> ObterMovimentosParaConciliacao()
        {
            IActionResult resposta;
            try
            {
                var itens = await ((IMovimentoBancarioServico)Servico).ObterMovimentosParaConciliacao();
                resposta = itens == null || itens.Count == 0
                    ? NotFound(new Resposta<IList<MovimentoBancario>>(
                        null,
                        "Nenhum registro localizado."))
                    : Ok(new Resposta<IList<MovimentoBancario>>(
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
