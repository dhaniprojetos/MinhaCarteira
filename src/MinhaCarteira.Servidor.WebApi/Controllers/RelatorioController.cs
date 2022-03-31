using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly RelatorioServico _servico;

        public RelatorioController(RelatorioServico servico)
        {
            _servico = servico;
        }

        [HttpGet("obter-extratos")]
        public async Task<IActionResult> ObterPorId()
        {
            IActionResult resposta;
            try
            {
                var itemDb = await _servico.ObterRelatorioSaldos();

                resposta = itemDb != null
                    ? Ok(new Resposta<ExtratoRelatorio>(
                        itemDb,
                        "Item localizado com sucesso."))
                    : NotFound(new Resposta<ExtratoRelatorio>(
                        default,
                        "Nenhum registro localizado."));
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            return resposta;
        }

    }
}
