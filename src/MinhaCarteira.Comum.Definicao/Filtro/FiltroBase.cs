using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Comum.Definicao.Filtro
{
    public struct FiltroOpcao
    {
        public FiltroOpcao(
            string nomePropriedade,
            TipoOperadorBusca operador,
            object valor)
        {
            NomePropriedade = nomePropriedade;
            Valor = valor;
            Operador = operador;
        }

        public string NomePropriedade { get; set; }
        public object Valor { get; set; }
        public TipoOperadorBusca Operador { get; set; }
    }

    public class FiltroBase<TEntidade> : ICriterio<TEntidade>
    {
        public FiltroBase()
        {
            AdicionarIncludes = true;
            OpcoesFiltro = new List<FiltroOpcao>();
        }

        public bool AdicionarIncludes { get; set; }
        public IList<FiltroOpcao> OpcoesFiltro { get; set; }

        public int Pagina { get; set; } = 1;
        public int ItensPorPagina { get; set; } = 1000;
    }
}
