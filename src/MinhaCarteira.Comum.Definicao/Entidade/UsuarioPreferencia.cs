namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class UsuarioPreferencia
    {
        public UsuarioPreferencia()
        {
            //construtor vazio para o ORM
        }
        public UsuarioPreferencia(int usuarioId, string nome, string valor)
        {
            Nome = nome;
            Valor = valor;
            UsuarioId = usuarioId;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
