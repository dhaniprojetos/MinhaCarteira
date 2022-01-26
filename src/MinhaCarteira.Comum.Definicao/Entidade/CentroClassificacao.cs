using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class CentroClassificacao : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        public bool EhCusto { get; set; }
        public bool EhReceita { get; set; }
    }
}
