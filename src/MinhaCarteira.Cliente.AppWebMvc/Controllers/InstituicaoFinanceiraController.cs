using System.IO;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class InstituicaoFinanceiraController :
        BaseController<InstituicaoFinanceira, InstituicaoFinanceiraViewModel>
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public InstituicaoFinanceiraController(
            IServicoBase<InstituicaoFinanceira> servico,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
            : base(servico, mapper)
        {
            this.webHostEnvironment = webHostEnvironment;
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

        private string UploadedFile(InstituicaoFinanceiraViewModel model)
        {
            if (model.Icone == null) return null;

            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid() + "_" + model.Icone.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            model.Icone.CopyTo(fileStream);

            return uniqueFileName;
        }

        protected override Tuple<InstituicaoFinanceiraViewModel, InstituicaoFinanceira> ExecutarAntesSalvar(InstituicaoFinanceiraViewModel viewModel, InstituicaoFinanceira model)
        {
            var uniqueFileName = UploadedFile(viewModel);
            model.Icone = uniqueFileName;

            return new Tuple<InstituicaoFinanceiraViewModel, InstituicaoFinanceira>(viewModel, model);
        }
    }
}
