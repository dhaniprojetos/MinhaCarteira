using System.ComponentModel;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class PessoaViewModel : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        [DisplayName("Cliente")]
        public bool EhCliente { get; set; }
        [DisplayName("Fornecedor")]
        public bool EhFornecedor { get; set; }
    }
}
