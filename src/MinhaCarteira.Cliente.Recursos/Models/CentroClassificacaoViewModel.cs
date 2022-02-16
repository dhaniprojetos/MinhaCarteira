using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class CentroClassificacaoViewModel : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        public bool EhDespesa { get; set; }
        public bool EhReceita { get; set; }
    }
}
