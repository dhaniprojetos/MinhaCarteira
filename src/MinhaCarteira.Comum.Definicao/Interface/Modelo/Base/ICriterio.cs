using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Filtro;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo.Base
{
    public interface ICriterio
    {
        bool AdicionarIncludes { get; set; }
        IList<FiltroOpcao> OpcoesFiltro { get; set; }
        string Ordenacao { get; set; }

        int Pagina { get; set; }
        int ItensPorPagina { get; set; }
    }
}
