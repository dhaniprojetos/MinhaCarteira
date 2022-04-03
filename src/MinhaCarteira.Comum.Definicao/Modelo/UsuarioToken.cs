using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Modelo
{
    public class UsuarioToken
    {
        public string Usuario { get; set; }
        public string TokenAcesso { get; set; }
        public List<string> Papeis { get; set; }
    }
}
