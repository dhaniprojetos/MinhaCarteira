using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Nome { get; set; }
        public int? IdAuxiliar { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }
        public int? IdCategoriaPai { get; set; }
        public CategoriaViewModel CategoriaPai { get; set; }
        public string NomeCategoriaPai => CategoriaPai != null 
            ? CategoriaPai.Nome 
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
                    Text = s.Nome
                })
                .ToList();

            Categorias = instituicoes;
        }
    }
}
