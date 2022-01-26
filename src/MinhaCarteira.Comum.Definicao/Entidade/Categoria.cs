using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Categoria : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? IdAuxiliar { get; set; }

        public int? IdCategoriaPai { get; set; }
        public Categoria CategoriaPai { get; set; }
        public IList<Categoria> SubCategoria { get; set; }

        public string Caminho => GetCaminho(this);
        
        private string GetCaminho(Categoria obj)
        {
            return obj.CategoriaPai == null
                ? obj.Nome
                : $"{GetCaminho(obj.CategoriaPai)} \\ {obj.Nome}";
        }

        public Categoria()
        {
            SubCategoria = new List<Categoria>();
        }
    }
}
