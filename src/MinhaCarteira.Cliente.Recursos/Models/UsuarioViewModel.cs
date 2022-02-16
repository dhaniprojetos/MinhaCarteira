using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class UsuarioViewModel : IEntidade
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string TokenAcesso { get; set; }
    }
}
