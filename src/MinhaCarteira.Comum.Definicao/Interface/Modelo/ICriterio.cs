using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface ICriterio<TEntidade>
    {
        bool AdicionarIncludes { get; set; }
        IList<KeyValuePair<string, string>> Filtros { get; set; }
    }
}
