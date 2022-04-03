using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class ContaBancariaController : 
        BaseController<ContaBancaria, IContaBancariaServico>
    {
        public ContaBancariaController(IContaBancariaServico servico) : base(servico)
        {
        }

        [HttpPost]
        [Route("atualizar-saldo-conta")]
        public async Task<IActionResult> AtualizarSaldoConta(string idsContaBancaria)
        {
            IActionResult resposta;
            try
            {
                var bemSucedido = await Servico.AtualizarSaldoConta(idsContaBancaria);
                resposta = !bemSucedido
                    ? NotFound(new Resposta<bool>(
                        false,
                        "Não foi possível atualizar o saldo da conta."))
                    : Ok(new Resposta<bool>(
                        bemSucedido,
                        $"Saldos da conta {idsContaBancaria} atualizados com sucesso."));
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
