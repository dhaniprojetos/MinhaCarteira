using System.Collections.Generic;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class UsuarioTokenViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string NomeCompleto =>
            $"{Nome} {Sobrenome}";

        public string TokenAcesso { get; set; }
        public IList<string> Roles { get; set; }
    }
}
