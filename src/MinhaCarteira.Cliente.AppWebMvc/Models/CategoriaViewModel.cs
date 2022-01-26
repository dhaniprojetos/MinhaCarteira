using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class CategoriaViewModel : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? IdAuxiliar { get; set; }

        public int? IdCategoriaPai { get; set; }
        public CategoriaViewModel CategoriaPai { get; set; }
    }
}
