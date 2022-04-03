using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Papel : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public IList<UsuarioPapel> Usuarios { get; set; }
    }
}
