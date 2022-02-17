using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class AgendamentoController : BaseController<Agendamento, AgendamentoViewModel>
    {
        public AgendamentoController(
            IServicoBase<Agendamento> servico,
            IMapper mapper)
            : base(servico, mapper) { }

        protected override async Task<AgendamentoViewModel> InicializarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(viewModel);
        }
        protected override async Task<bool> ValidarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(true);
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index()
        {
            return await base.Index();
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
