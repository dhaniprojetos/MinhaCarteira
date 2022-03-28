using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Usuario : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}
