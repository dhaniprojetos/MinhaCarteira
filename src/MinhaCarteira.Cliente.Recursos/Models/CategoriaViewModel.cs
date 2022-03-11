using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using MinhaCarteira.Cliente.Recursos.Attributes;
using MinhaCarteira.Cliente.Recursos.Models.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class CategoriaViewModel : BaseViewModel, IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string Icone { get; set; }
        public string IconeCategoria { get; set; }
        public string NomeArquivo { get; set; }
        public string NomeArquivoCategoria { get; set; }
        public string MimeType
        {
            get
            {
                new FileExtensionContentTypeProvider()
                    .TryGetContentType(NomeArquivoCategoria, out string contentType);

                return contentType ?? "image/svg+xml";
            }
        }

        [MaxFileSize("UploadTamanhoMaximo")]
        [AllowedExtensions("UploadExtensaoPermitida")]
        public IFormFile IconeForm { get; set; }

        private int? idCategoriaPai;
        public int? IdCategoriaPai
        {
            get => idCategoriaPai;
            set
            {
                idCategoriaPai = value;
                if (value == null || value == 0)
                {
                    CategoriaPai = null;
                }
                else
                {
                    if (CategoriaPai != null)
                        CategoriaPai.Id = (int)value;
                }
            }
        }
        public CategoriaViewModel CategoriaPai { get; set; }
        public IList<CategoriaViewModel> SubCategoria { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }
        public string NomeCategoriaPai => CategoriaPai != null
            ? CategoriaPai.Caminho
            : string.Empty;

        public CategoriaViewModel()
        {
            Categorias = new List<SelectListItem>();
        }
        public CategoriaViewModel(Resposta<IList<Categoria>> resposta)
        {
            AdicionarCategorias(resposta.Dados);
        }

        public void AdicionarCategorias(IList<Categoria> items)
        {
            var instituicoes = items?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Caminho
                })
                .OrderBy(o => o.Text)
                .ToList();

            Categorias = instituicoes;
        }
    }
}
