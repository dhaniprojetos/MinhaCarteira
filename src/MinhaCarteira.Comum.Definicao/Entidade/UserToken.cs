using System;
using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class UserToken
    {
        public IList<string> Roles { get; set; }
        public string Username { get; set; }
        public string TokenAcesso { get; set; }
        public DateTime Expiration { get; set; }
    }
}
