using System;

namespace MinhaCarteira.Comum.Definicao.Modelo
{
    public class LoginToken
    {
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string TokenAcesso { get; set; }
        public DateTime Expiration { get; set; }
    }
}
