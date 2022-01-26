using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Recursos.Refit.Base;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class CategoriaController : BaseController<Categoria, CategoriaViewModel>
    {
        private readonly IServicoBase<Categoria> _categoriaServico;

        public CategoriaController(
            IServicoBase<Categoria> servico,
            IMapper mapper, IServicoBase<Categoria> categoriaServico)
            : base(servico, mapper)
        {
            _categoriaServico = categoriaServico;
        }

        protected override async Task<CategoriaViewModel> InicializarViewModel(CategoriaViewModel viewModel)
        {
            var resp = await _categoriaServico.Navegar();
            viewModel.AdicionarCategorias(resp.Dados);

            return await Task.FromResult(viewModel);
        }

        protected override async Task<bool> ValidarViewModel(CategoriaViewModel viewModel)
        {
            return await Task.FromResult(true);
        }

        [HttpPost]
        public async Task<JsonResult> ObterCategorias(string prefix)
        {
            var resp = await _categoriaServico.Navegar();

            var items = resp.Dados
                .Select(s => new { label = s.Caminho, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index()
        {
            var resposta = await Servico.Navegar();
            IList<CategoriaViewModel> itens = null;

            if (resposta.Dados != null)
                itens = Mapper.Map<List<CategoriaViewModel>>(resposta.Dados);

            return View(itens?.OrderBy(o => o.Caminho));
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
