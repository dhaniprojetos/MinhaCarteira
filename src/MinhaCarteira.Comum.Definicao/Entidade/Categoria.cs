using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Categoria : IEntidade
    {
        public Categoria()
        {
            SubCategoria = new List<Categoria>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int? IdAuxiliar { get; set; }

        public int? IdCategoriaPai { get; set; }
        public Categoria CategoriaPai { get; set; }
        public IList<Categoria> SubCategoria { get; set; }
    }
}
