using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Helper;
using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Modelo
{
    public class UsuarioToken
    {
        public UsuarioToken()
        {
            //construtor necessário para a injeção de dependencia
        }

        public UsuarioToken(Usuario source)
        {
            this.Mapear(source);
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        
        public string TokenAcesso { get; set; }
        public IList<string> Roles { get; set; }
        public IDictionary<string, string> Preferences { get; set; }
    }
}
