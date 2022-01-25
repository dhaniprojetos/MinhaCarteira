﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Recursos.Refit.Base;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class InstituicaoFinanceiraController :
        BaseController<InstituicaoFinanceira, InstituicaoFinanceiraViewModel>
    {
        public InstituicaoFinanceiraController(IServicoBase<InstituicaoFinanceira> servico, IMapper mapper) : base(servico, mapper)
        {
        }

        protected override async Task<InstituicaoFinanceiraViewModel> InicializarViewModel(InstituicaoFinanceiraViewModel viewModel)
        {
            return await Task.FromResult(viewModel);
        }

        protected override async Task<bool> ValidarViewModel(InstituicaoFinanceiraViewModel viewModel)
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