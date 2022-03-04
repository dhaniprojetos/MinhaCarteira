using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Filtro
{
    public class FiltroBase<TEntidade> : ICriterio<TEntidade>
    {
        public bool AdicionarIncludes { get; set; }
        public IList<KeyValuePair<string, string>> Filtros { get; set; }
    }
}
