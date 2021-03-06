using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Cliente.Recursos.Models.Base;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using X.PagedList;

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

        protected override async Task<Tuple<int, IList<CategoriaViewModel>>> ObterTodos(ICriterio criterio)
        {
            criterio.ItensPorPagina = 5;
            criterio.OpcoesFiltro.Add(
                new FiltroOpcao("IdCategoriaPai", Comum.Definicao.Modelo.TipoOperadorBusca.Igual, null, false));

            var resposta = await Servico.Navegar(criterio);
            var itens = resposta.Dados;
            var itensExpandidos = resposta.Dados
                .SelectMany(s => s.SubCategoria)
                .ToList();
            
            itensExpandidos
                .ForEach(f => f.CategoriaPai = itens.FirstOrDefault(item => item.Id == f.IdCategoriaPai));

            itensExpandidos.AddRange(itens);

            var itensViewModel = Mapper.Map<List<CategoriaViewModel>>(
                itensExpandidos);

            return new Tuple<int, IList<CategoriaViewModel>>(
                resposta.TotalRegistros,
                itensViewModel?.OrderBy(o => o.Caminho).ToList());
        }
        protected override async Task<CategoriaViewModel> InicializarViewModel(CategoriaViewModel viewModel)
        {
            var resp = await _categoriaServico.Navegar(null);
            viewModel.AdicionarCategorias(resp.Dados);

            return await Task.FromResult(viewModel);
        }
        protected override async Task<bool> ValidarViewModel(CategoriaViewModel viewModel)
        {
            return await Task.FromResult(true);
        }
        protected override async Task<Tuple<CategoriaViewModel, Categoria>> ExecutarAntesSalvar(CategoriaViewModel viewModel, Categoria model)
        {
            if (viewModel.IconeForm?.Length > 0)
            {
                using var ms = new MemoryStream();
                viewModel.IconeForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                model.Icone = Convert.ToBase64String(fileBytes);
                model.NomeArquivo = viewModel.IconeForm.FileName;
            }

            if (viewModel.Id == 0)
                return await base.ExecutarAntesSalvar(viewModel, model);

            var itemDb = await ObterPorId(viewModel.Id);
            var itemMap = Mapper.Map<Categoria>(itemDb);
            model.SubCategoria = itemMap.SubCategoria;

            return await base.ExecutarAntesSalvar(viewModel, model);
        }

        [HttpPost]
        public async Task<JsonResult> ObterCategorias(string prefix)
        {
            var resp = await _categoriaServico.Navegar(null);

            var items = resp.Dados
                .Select(s => new { label = s.Caminho, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index(int? page, string filtroJson, ListaBaseViewModel<CategoriaViewModel> model)
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
