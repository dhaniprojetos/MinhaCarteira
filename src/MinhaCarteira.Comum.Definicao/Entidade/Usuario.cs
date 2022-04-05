using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Usuario : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public IList<UsuarioPapel> Papeis { get; set; }
        public IList<UsuarioPreferencia> Preferencias { get; set; }
    }
}
