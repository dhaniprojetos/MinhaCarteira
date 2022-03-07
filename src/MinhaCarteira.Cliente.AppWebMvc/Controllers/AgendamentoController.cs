using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Comum.Definicao.Filtro;
using X.PagedList;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class AgendamentoController : BaseController<Agendamento, AgendamentoViewModel>
    {
        private readonly IMovimentoServico _movimentoServico;

        public AgendamentoController(
            IAgendamentoServico servico,
            IMapper mapper,
            IMovimentoServico movimentoServico)
            : base(servico, mapper)
        {
            _movimentoServico = movimentoServico;
        }

        protected override async Task<AgendamentoViewModel> InicializarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(viewModel);
        }
        protected override async Task<bool> ValidarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(true);
        }

        private async Task<AgendamentoItemViewModel> ObterParcelaPorId(int id)
        {
            try
            {
                var resposta = await ((IAgendamentoServico)Servico).ObterParcelaPorId(id);
                var parcela = Mapper.Map<AgendamentoItemViewModel>(resposta.Dados);

                if (!TempData.ContainsKey("RetornoApi")) return parcela;
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return parcela;
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                if (retornoApi == null) return new AgendamentoItemViewModel();

                retornoApi.BemSucedido = false;
                if (!TempData.ContainsKey("RetornoApi"))
                    ViewBag.RetornoApi = retornoApi;
                else
                {
                    var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                    ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);
                }

                return new AgendamentoItemViewModel();
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return new AgendamentoItemViewModel();
            }
        }

        public async Task<IActionResult> PagarParcela(int id)
        {
            var item = await ObterParcelaPorId(id);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> PagarParcela(AgendamentoItemViewModel item)
        {
            if (!ModelState.IsValid)
            {
                var itemDb = await ObterParcelaPorId(item.Id);
                itemDb.Data = item.Data;
                itemDb.Valor = item.Valor;

                return View(itemDb);
            }

            try
            {
                var itemApi = Mapper.Map<AgendamentoItem>(item);
                itemApi.AgendamentoId = item.Agendamento.Id;
                var retornoApi = await ((IAgendamentoServico)Servico).BaixarParcela(itemApi);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>()
                              ?? new Resposta<Exception>(ex, ex.Message);

                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> ObterMovimentos(string prefix)
        {
            var resp = await _movimentoServico.ObterMovimentosParaConciliacao();

            return Json(resp.Dados);
        }

        public async Task<IActionResult> ConciliarParcela(int id)
        {
            var item = await ObterParcelaPorId(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> ConciliarParcela(int id, string idMovimentos)
        {
            if (!ModelState.IsValid)
            {
                var item = await ObterParcelaPorId(id);
                return View(item);
            }

            try
            {
                var retornoApi = await ((IAgendamentoServico)Servico).ConciliarParcela(id, idMovimentos);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>()
                              ?? new Resposta<Exception>(ex, ex.Message);

                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }

            return Json(new { redirectToUrl = Url.Action("Index", "Agendamento") });
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index(int? page)
        {
            try
            {
                var criterio = new FiltroBase<AgendamentoViewModel>()
                {
                    Pagina = page ?? 1
                };

                var resposta = await ((IAgendamentoServico)Servico).ContasAVencer(90);
                var itens = Mapper.Map<List<AgendamentoItemViewModel>>(resposta.Dados);
                var itensPagedList = await itens
                    .ToPagedListAsync(criterio.Pagina, criterio.ItensPorPagina);

                if (!TempData.ContainsKey("RetornoApi")) return View(itensPagedList);
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return View(itensPagedList);
            }
            catch (Refit.ApiException ex)
            {
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

                return View(new List<AgendamentoItemViewModel>().ToPagedList());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new List<AgendamentoItemViewModel>().ToPagedList());
            }
        }

        public override async Task<IActionResult> Criar()
        {
            return await base.Criar();
        }

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
