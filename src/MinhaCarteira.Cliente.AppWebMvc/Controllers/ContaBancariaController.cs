using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Recursos.Refit.Base;
using MinhaCarteira.Servidor.Controle.Servico;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class ContaBancariaController
        : BaseController<ContaBancaria, ContaBancariaViewModel>
    {
        private readonly IServicoBase<InstituicaoFinanceira> _instituicaoFinanceiraServico;

        public ContaBancariaController(
            IServicoBase<ContaBancaria> servico,
            IMapper mapper,
            IServicoBase<InstituicaoFinanceira> instituicaoFinanceiraServico)
            : base(servico, mapper)
        {
            _instituicaoFinanceiraServico = instituicaoFinanceiraServico;
        }

        protected override async Task<ContaBancariaViewModel> InicializarViewModel(ContaBancariaViewModel viewModel)
        {
            var resp = await _instituicaoFinanceiraServico.Navegar();
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
            var resp = await _instituicaoFinanceiraServico.Navegar();

            var items = resp.Dados
                .Select(s => new { label = s.Nome, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        #region Métodos sobrescritos apenas manter as views
        public override Task<IActionResult> Index()
        {
            return base.Index();
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
