using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class CategoriaViewModel : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }

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
