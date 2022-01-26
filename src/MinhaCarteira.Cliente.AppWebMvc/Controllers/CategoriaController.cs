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
