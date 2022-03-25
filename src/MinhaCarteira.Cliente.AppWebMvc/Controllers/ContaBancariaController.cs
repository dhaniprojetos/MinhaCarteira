using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using System;
using System.Linq;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;
using X.PagedList;
using MinhaCarteira.Cliente.Recursos.Models.Base;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class ContaBancariaController
        : BaseController<ContaBancaria, ContaBancariaViewModel>
    {
        private readonly IServicoBase<InstituicaoFinanceira> _instituicaoFinanceiraServico;

        public ContaBancariaController(
            IContaBancariaServico servico,
            IMapper mapper,
            IServicoBase<InstituicaoFinanceira> instituicaoFinanceiraServico)
            : base(servico, mapper)
        {
            _instituicaoFinanceiraServico = instituicaoFinanceiraServico;
        }

        protected override async Task<ContaBancariaViewModel> InicializarViewModel(ContaBancariaViewModel viewModel)
        {
            var resp = await _instituicaoFinanceiraServico.Navegar(null);
            viewModel.AdicionarInstituicoesFinanceiras(resp.Dados);

            return viewModel;
        }

        protected override async Task<bool> ValidarViewModel(ContaBancariaViewModel conta)
        {
            if (!string.IsNullOrWhiteSpace(conta.Nome))
                return await Task.FromResult(true);

            ModelState.AddModelError(
                nameof(conta.Nome),
                "O campo Nome deve ser preenchido.");

            return await Task.FromResult(false);
        }

        [HttpPost]
        public async Task<JsonResult> ObterInstituicoesFinanceira(string prefix)
        {
            var resp = await _instituicaoFinanceiraServico.Navegar(null);

            var items = resp.Dados
                .Select(s => new { label = s.Nome, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarSaldoConta(string id)
        {
            try
            {
                var resp = await ((IContaBancariaServico)Servico).AtualizarSaldoConta(id);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(resp);
            }
            catch (Refit.ApiException ex)
            {
                TempData["RetornoApi"] = ex.Content;
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            
            return RedirectToAction(nameof(Index));
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index(int? page, string filtroJson, ListaBaseViewModel<ContaBancariaViewModel> model)
        {
            return await base.Index(page, filtroJson, model);
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
