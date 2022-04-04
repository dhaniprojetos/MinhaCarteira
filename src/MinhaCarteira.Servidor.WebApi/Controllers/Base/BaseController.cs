using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;

namespace MinhaCarteira.Servidor.WebApi.Controllers.Base
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TEntidade, TServico, TRepositorio> : ControllerBase
        where TEntidade : class, IEntidade
        where TRepositorio : ICrud<TEntidade>
        where TServico : IServicoCrud<TEntidade, TRepositorio>
    {
        public BaseController(TServico servico)
        {
            Servico = servico;
        }

        protected static void DefinirCodigoStatus(ref IActionResult resposta)
        {
            var objResult = (ObjectResult)resposta;
            if (objResult != null && objResult.Value is IRespostaServico resp)
            {
                resp.StatusCode = objResult.StatusCode;
                if (resp.StatusCode >= 400 && resp.Mensagem != "Nenhum registro localizado.")
                    resp.BemSucedido = false;
            }
        }

        protected TServico Servico { get; }

        [HttpGet]
        public async Task<IActionResult> Navegar([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] FiltroBase criterio)
        {
            IActionResult resposta;
            try
            {
                criterio ??= new FiltroBase();

                var itens = await Servico.Navegar(criterio);

                resposta = itens == null || itens.Item2.Count == 0
                    ? NotFound(new RespostaPaginada<IList<TEntidade>>(
                        null,
                        criterio.Pagina,
                        criterio.ItensPorPagina,
                        itens.Item1,
                        "Nenhum registro localizado."))
                    : Ok(new RespostaPaginada<IList<TEntidade>>(
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
                        "Falha ao tentar cadastrar os itens enviados."));
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
                        "Falha ao tentar alterar os itens enviados."));
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            DefinirCodigoStatus(ref resposta);
            return resposta;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Incluir(TEntidade item)
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
                        "Falha ao tentar cadastrar o item enviado."));
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
                        "Falha ao tentar alterar o item enviado."));

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
                        "Falha ao tentar remover o item enviado."));
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