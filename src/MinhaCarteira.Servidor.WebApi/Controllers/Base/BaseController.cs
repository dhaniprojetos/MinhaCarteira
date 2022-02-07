using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;

namespace MinhaCarteira.Servidor.WebApi.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TEntidade> : ControllerBase
    {
        public BaseController(IServicoCrud<TEntidade> servico)
        {
            Servico = servico;
        }

        private static void DefinirCodigoStatus(ref IActionResult resposta)
        {
            var objResult = (ObjectResult)resposta;
            if (objResult != null && objResult.Value is IRespostaServico resp)
                resp.StatusCode = objResult.StatusCode;
        }

        protected IServicoCrud<TEntidade> Servico { get; }

        [HttpGet]
        public async Task<IActionResult> Navegar()
        {
            IActionResult resposta;
            try
            {
                var itens = await Servico.Navegar(null);
                resposta = itens == null || itens.Count == 0
                    ? NotFound(new Resposta<IList<TEntidade>>(
                        null,
                        "Nenhum registro localizado."))
                    : Ok(new Resposta<IList<TEntidade>>(
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            IActionResult resposta;
            try
            {
                var itemDb = await Servico.ObterPorId(id);

                resposta = itemDb != null
                    ? Ok(new Resposta<TEntidade>(
                        itemDb,
                        "Item localizado com sucesso."))
                    : NotFound(new Resposta<TEntidade>(
                        default,
                        "Nenhum registro localizado."));
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [HttpPost("IncluirRange")]
        public async Task<IActionResult> IncluirRange(IList<TEntidade> itens)
        {
            IActionResult resposta;
            try
            {
                var itemDb = await Servico.IncluirRange(itens);

                resposta = itemDb != null
                    ? CreatedAtAction(
                        nameof(IncluirRange),
                        new Resposta<IList<TEntidade>>(
                            itemDb,
                            "Itens cadastrados com sucesso."))
                    : NotFound(new Resposta<IList<TEntidade>>(
                        null,
                        "Falha ao tentar cadastrar os itens enviados.")
                    {
                        BemSucedido = false
                    });
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [HttpPost("AlterarRange")]
        public async Task<IActionResult> AlterarRange(IList<TEntidade> itens)
        {
            IActionResult resposta;
            try
            {
                var itemDb = await Servico.AlterarRange(itens);

                resposta = itemDb != null
                    ? Ok(new Resposta<IList<TEntidade>>(
                        itemDb,
                        "Itens alterados com sucesso."))
                    : NotFound(new Resposta<IList<TEntidade>>(
                        null,
                        "Falha ao tentar alterar os itens enviados.")
                    {
                        BemSucedido = false
                    });
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [HttpPost]
        public async Task<IActionResult> Incluir(TEntidade item)
        {
            IActionResult resposta;
            try
            {
                var itemDb = await Servico.Incluir(item);

                resposta = itemDb != null
                    ? CreatedAtAction(
                        nameof(IncluirRange),
                        new Resposta<TEntidade>(
                            itemDb,
                            "Item cadastrado com sucesso."))
                    : NotFound(new Resposta<IList<TEntidade>>(
                        null,
                        "Falha ao tentar cadastrar o item enviado.")
                    {
                        BemSucedido = false
                    });
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(TEntidade item)
        {
            IActionResult resposta;
            try
            {
                var itemDb = await Servico.Alterar(item);

                resposta = itemDb != null
                    ? Ok(new Resposta<TEntidade>(
                        itemDb,
                        "Item alterado com sucesso."))
                    : NotFound(new Resposta<IList<TEntidade>>(
                        null,
                        "Falha ao tentar alterar o item enviado.")
                    {
                        BemSucedido = false
                    });

                return resposta;
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int id)
        {
            IActionResult resposta;
            try
            {
                var linhasAfetadas = await Servico.Deletar(id);
                var msg = linhasAfetadas > 1
                    ? "itens removidos"
                    : "item removido";

                resposta = linhasAfetadas > 0
                    ? Ok(new Resposta<int>(
                        linhasAfetadas,
                        $"{linhasAfetadas} {msg} com sucesso."))
                    : NotFound(new Resposta<IList<TEntidade>>(
                        null,
                        "Falha ao tentar remover o item enviado.")
                    {
                        BemSucedido = false
                    });
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