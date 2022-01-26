using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.ViewModel
{
    public class PessoaViewModel : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        public bool EhCliente { get; set; }
        public bool EhFornecedor { get; set; }
    }
}
