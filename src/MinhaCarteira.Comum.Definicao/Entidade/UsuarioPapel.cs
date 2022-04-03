using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class UsuarioPapel : IEntidade
    {
        public int Id { get; set; }
        public int PapelId { get; set; }
        public int UsuarioId { get; set; }

        public Papel Papel { get; set; }
        public Usuario Usuario { get; set; }
    }
}
