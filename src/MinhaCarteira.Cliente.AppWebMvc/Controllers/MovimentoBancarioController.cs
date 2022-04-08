using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;
using Refit;
using X.PagedList;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class MovimentoBancarioController : BaseController<MovimentoBancario, MovimentoBancarioViewModel>
    {
        private readonly IServicoBase<Pessoa> _pessoaServico;
        private readonly IServicoBase<CentroClassificacao> _centroClassificacaoServico;
        private readonly IServicoBase<Categoria> _categoriaServico;
        private readonly IContaBancariaServico _contaBancariaServico;

        public MovimentoBancarioController(
            IMovimentoServico servico,
            IMapper mapper,
            IServicoBase<Pessoa> pessoaServico,
            IServicoBase<CentroClassificacao> centroClassificacaoServico,
            IServicoBase<Categoria> categoriaServico,
            IContaBancariaServico contaBancariaServico)
            : base(servico, mapper)
        {
            _pessoaServico = pessoaServico;
            _centroClassificacaoServico = centroClassificacaoServico;
            _categoriaServico = categoriaServico;
            _contaBancariaServico = contaBancariaServico;
        }

        protected override async Task<MovimentoBancarioViewModel> InicializarViewModel(MovimentoBancarioViewModel viewModel)
        {
            return await Task.FromResult(viewModel);
        }

        protected override async Task<bool> ValidarViewModel(MovimentoBancarioViewModel viewModel)
        {
            return await Task.FromResult(true);
        }

        [HttpPost]
        public async Task<JsonResult> ObterContaBancaria(string prefix)
        {
            var criterio = new FiltroBase
            {
                OpcoesFiltro = new List<FiltroOpcao>
                {
                    new FiltroOpcao("Nome", TipoOperadorBusca.Contem, prefix)
                }
            };

            try
            {
                var resp = await _contaBancariaServico.Navegar(criterio);

                var items = resp.Dados
                    .Select(s => new { label = s.Nome, val = s.Id })
                    .OrderBy(s => s.label)
                    .ToList();

                return Json(items);
            }
            catch (ApiException)
            {
                return Json(null);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ObterCategoria(string prefix)
        {
            var criterio = new FiltroBase
            {
                ItensPorPagina = 1
            };

            try
            {
                var resp = await _categoriaServico.Navegar(criterio);
                var items = resp.Dados
                    .Select(s => new { label = s.Caminho, val = s.Id })
                    .Where(w => string.IsNullOrEmpty(prefix) ||
                                w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(s => s.label)
                    .ToList();

                return Json(items);
            }
            catch (ApiException)
            {
                return Json(null);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ObterPessoa(string prefix)
        {
            var criterio = new FiltroBase
            {
                OpcoesFiltro = new List<FiltroOpcao>
                {
                    new FiltroOpcao("Nome", TipoOperadorBusca.Contem, prefix)
                }
            };

            try
            {
                var resp = await _pessoaServico.Navegar(criterio);

                var items = resp.Dados
                    .Select(s => new { label = s.Nome, val = s.Id })
                    .OrderBy(s => s.label)
                    .ToList();

                return Json(items);
            }
            catch (ApiException)
            {
                return Json(null);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ObterCentroClassificacao(string prefix)
        {
            var criterio = new FiltroBase
            {
                OpcoesFiltro = new List<FiltroOpcao>
                {
                    new FiltroOpcao("Nome", TipoOperadorBusca.Contem, prefix)
                }
            };

            try
            {
                var resp = await _centroClassificacaoServico.Navegar(criterio);

                var items = resp.Dados
                    .Select(s => new { label = s.Nome, val = s.Id })
                    .OrderBy(s => s.label)
                    .ToList();

                return Json(items);
            }
            catch (ApiException)
            {
                return Json(null);
            }
        }

        public async Task<IActionResult> Clonar(string id)
        {
            try
            {
                MovimentoBancarioViewModel item = await ObterPorId(int.Parse(id));

                return View(nameof(Criar), item);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(nameof(Criar), new MovimentoBancarioViewModel());
            }
        }

        private async Task<ListaMovimentoBancarioViewModel> ObterMovimentosConta(
            int idContaBancaria, ICriterio filtroMovimento)
        {
            var retornoApi = await _contaBancariaServico.Navegar(null);
            var contas = Mapper.Map<IList<ContaBancariaViewModel>>(retornoApi.Dados);
            var totalMovimentos = 0;

            var item = new ListaMovimentoBancarioViewModel()
            {
                Contas = contas,
                Filtro = filtroMovimento,
                Movimentos = new StaticPagedList<MovimentoBancarioViewModel>(
                    new List<MovimentoBancarioViewModel>(),
                    filtroMovimento.Pagina,
                    filtroMovimento.ItensPorPagina,
                    totalMovimentos)
            };

            IList<MovimentoBancarioViewModel> movimentos;
            try
            {
                var retornoMovimentos = await Servico.Navegar(filtroMovimento);
                totalMovimentos = retornoMovimentos.TotalRegistros;
                movimentos = Mapper.Map<IList<MovimentoBancarioViewModel>>(retornoMovimentos.Dados);

                item.Movimentos = new StaticPagedList<MovimentoBancarioViewModel>(
                    movimentos,
                    filtroMovimento.Pagina,
                    filtroMovimento.ItensPorPagina,
                    totalMovimentos);
            }
            catch
            {
                throw;
            }

            return item;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id, int? page, string filtroJson, ListaMovimentoBancarioViewModel model)
        {
            try
            {
                var idConta = int.Parse(id ?? "1");
                var filtro = string.IsNullOrWhiteSpace(filtroJson)
                    ? new FiltroBase()
                    : JsonConvert.DeserializeObject<FiltroBase>(filtroJson);

                if (!string.IsNullOrWhiteSpace(model.OpcaoAtual.NomePropriedade) &&
                    !string.IsNullOrWhiteSpace(model.OpcaoAtual.Valor))
                    filtro.OpcoesFiltro.Add(model.OpcaoAtual);

                var filtroMovimento = new FiltroBase()
                {
                    Pagina = page ?? 1,
                    Ordenacao = "DataMovimento desc, Id",
                    OpcoesFiltro = filtro.OpcoesFiltro.Distinct().ToList()
                };

                filtroMovimento.OpcoesFiltro.Add(new FiltroOpcao(
                    "ContaBancariaId",
                    TipoOperadorBusca.Igual,
                    idConta.ToString(),
                    false));

                var item = await ObterMovimentosConta(idConta, filtroMovimento);

                if (!TempData.ContainsKey("RetornoApi")) return View(item);
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return View(item);
            }
            catch (Refit.ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Logar", "Usuario");

                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                if (retornoApi == null)
                    retornoApi = new Resposta<Exception>(ex, ex.Message)
                    {
                        StatusCode = (int)ex.StatusCode
                    };

                if (!TempData.ContainsKey("RetornoApi"))
                    ViewBag.RetornoApi = retornoApi;
                else
                {
                    var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                    ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);
                }

                return View(new ListaMovimentoBancarioViewModel());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new ListaMovimentoBancarioViewModel());
            }
        }

        //[Route("criar")]
        [HttpGet]
        public async Task<IActionResult> Criar(string idContaBancaria)
        {
            var idConta = int.Parse(idContaBancaria ?? "1");
            var retornoApi = await _contaBancariaServico.ObterPorId(idConta);
            var conta = Mapper.Map<ContaBancariaViewModel>(retornoApi.Dados);

            var item = await InicializarViewModel(new MovimentoBancarioViewModel());
            item.ContaBancaria = conta;
            item.ContaBancariaId = conta.Id;

            return View(item);
        }

        [HttpPost]
        public override async Task<IActionResult> Criar(MovimentoBancarioViewModel item)
        {
            var retorno = await base.Criar(item);

            if (retorno is RedirectToActionResult)
                return RedirectToAction(nameof(Index), new { id = item.ContaBancariaId });

            return retorno;
        }

        [HttpPost]
        public async override Task<IActionResult> Alterar(MovimentoBancarioViewModel item)
        {
            var retorno = await base.Alterar(item);

            if (retorno is RedirectToActionResult)
                return RedirectToAction(nameof(Index), new { id = item.ContaBancariaId });

            return retorno;
        }

        [HttpPost]
        public async override Task<IActionResult> Deletar(MovimentoBancarioViewModel item)
        {
            var retorno = await base.Deletar(item);

            if (retorno is RedirectToActionResult)
                return RedirectToAction(nameof(Index), new { id = item.ContaBancariaId });

            return retorno;
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Detalhes(int id)
        {
            return await base.Detalhes(id);
        }

        public override async Task<IActionResult> Alterar(int id)
        {
            return await base.Alterar(id);
        }

        public override async Task<IActionResult> Deletar(int id)
        {
            return await base.Deletar(id);
        }
        #endregion
    }
}