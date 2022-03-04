using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Filtro;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface ICriterio<TEntidade>
    {
        bool AdicionarIncludes { get; set; }
        IList<FiltroOpcao> OpcoesFiltro { get; set; }
    }
}
