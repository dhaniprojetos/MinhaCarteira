using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class InstituicaoFinanceira : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string NomeArquivo { get; set; }
    }
}
