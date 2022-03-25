﻿using System.IO;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Cliente.Recursos.Models.Base;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class InstituicaoFinanceiraController :
        BaseController<InstituicaoFinanceira, InstituicaoFinanceiraViewModel>
    {
        public InstituicaoFinanceiraController(
            IServicoBase<InstituicaoFinanceira> servico,
            IMapper mapper)
            : base(servico, mapper) { }

        protected override async Task<Tuple<InstituicaoFinanceiraViewModel, InstituicaoFinanceira>> ExecutarAntesSalvar(InstituicaoFinanceiraViewModel viewModel, InstituicaoFinanceira model)
        {
            //var uniqueFileName = UploadedFile(viewModel);
            //model.Icone = uniqueFileName;
            if (viewModel.IconeForm?.Length > 0)
            {
                using var ms = new MemoryStream();
                viewModel.IconeForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                model.Icone = Convert.ToBase64String(fileBytes);
                model.NomeArquivo = viewModel.IconeForm.FileName;
            }

            return await Task.FromResult(
                new Tuple<InstituicaoFinanceiraViewModel, InstituicaoFinanceira>(viewModel, model));
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
        public override async Task<IActionResult> Index(int? page, string filtroJson, ListaBaseViewModel<InstituicaoFinanceiraViewModel> model)
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
