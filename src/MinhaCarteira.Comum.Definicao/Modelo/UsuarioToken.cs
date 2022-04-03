using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Modelo
{
    public class UsuarioToken
    {
        public string Usuario { get; set; }
        public string TokenAcesso { get; set; }
        public List<string> Papeis { get; set; }
    }
}
