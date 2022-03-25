using System;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class UserToken
    {
        public string Roles { get; set; }
        public string Username { get; set; }
        public string TokenAcesso { get; set; }
        public DateTime Expiration { get; set; }
    }
}
