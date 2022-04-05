namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class UsuarioPreferencia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
