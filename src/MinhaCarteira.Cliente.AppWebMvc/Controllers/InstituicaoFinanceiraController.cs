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
using Microsoft.Extensions.Configuration;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class InstituicaoFinanceiraController :
        BaseController<InstituicaoFinanceira, InstituicaoFinanceiraViewModel>
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InstituicaoFinanceiraController(
            IServicoBase<InstituicaoFinanceira> servico,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration config)
            : base(servico, mapper)
        {
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        private string UploadedFile(InstituicaoFinanceiraViewModel model)
        {
            if (model.IconeForm == null) return null;

            var destinoConfig = _config
                .GetSection("DefinicaoArquivos")["UploadRepositorioImagens"]
                .ToString();

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!string.IsNullOrWhiteSpace(destinoConfig))
                uploadsFolder = destinoConfig;

            uploadsFolder = Path.Combine(uploadsFolder, "instituicaoFinanceira");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + "_" + model.IconeForm.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            model.IconeForm.CopyTo(fileStream);

            return uniqueFileName;
        }
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
        //protected override async Task<IList<InstituicaoFinanceiraViewModel>> ObterTodos()
        //{
        //    var src = _config
        //        .GetSection("DefinicaoArquivos")["UploadRepositorioImagens"];
        //
        //    src = Path.Combine(src, "instituicaoFinanceira");
        //    src = src.Replace("wwwroot/", "");
        //    
        //    var itens = await base.ObterTodos();
        //    itens.ToList().ForEach(f => f.PathImagens = src);
        //
        //    return itens;
        //}

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
