using System.ComponentModel;
using MinhaCarteira.Cliente.Recursos.Models.Base;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class CentroClassificacaoViewModel : BaseViewModel, IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public string Nome { get; set; }
        [DisplayName("Depesa")]
        public bool EhDespesa { get; set; }
        [DisplayName("Receita")]
        public bool EhReceita { get; set; }
    }
}
