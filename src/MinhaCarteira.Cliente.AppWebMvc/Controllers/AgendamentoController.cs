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
using MinhaCarteira.Cliente.Recursos.Refit.Base;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class AgendamentoController : BaseController<Agendamento, AgendamentoViewModel>
    {
        private readonly IServicoBase<MovimentoBancario> _movimentoServico;

        public AgendamentoController(
            IAgendamentoServico servico,
            IMapper mapper,
            IServicoBase<MovimentoBancario> movimentoServico)
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
            if (!ModelState.IsValid) return View(item);

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
            var resp = await _movimentoServico.Navegar();

            return Json(resp.Dados);
            //var resp = await _categoriaServico.Navegar();
            //
            //var items = resp.Dados
            //    .Select(s => new { label = s.Caminho, val = s.Id })
            //    .Where(w => string.IsNullOrEmpty(prefix) ||
            //                w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
            //    .OrderBy(s => s.label)
            //    .ToList();
            //
            //return Json(items);
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
        public override async Task<IActionResult> Index()
        {
            try
            {
                var resposta = await ((IAgendamentoServico)Servico).ContasAVencer(30);
                var itens = Mapper.Map<List<AgendamentoItemViewModel>>(resposta.Dados);

                if (!TempData.ContainsKey("RetornoApi")) return View(itens);
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return View(itens);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                if (!TempData.ContainsKey("RetornoApi"))
                    ViewBag.RetornoApi = retornoApi;
                else
                {
                    var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                    ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);
                }

                return View(new List<AgendamentoItemViewModel>());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new List<AgendamentoItemViewModel>());
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
