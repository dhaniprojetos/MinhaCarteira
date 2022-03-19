using System.Collections.Generic;
using System.Text.Json;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Comum.Definicao.Filtro
{
    public class FiltroOpcao
    {
        public FiltroOpcao()
        {
            Operador = TipoOperadorBusca.Contem;
            NomePropriedade = string.Empty;
            Valor = string.Empty;
            Visivel = true;
        }
        public FiltroOpcao(
            string nomePropriedade,
            TipoOperadorBusca operador,
            string valor,
            bool visivel = true)
        {
            NomePropriedade = nomePropriedade;
            Valor = valor;
            Operador = operador;
            Visivel = visivel;
        }

        public bool Visivel { get; set; } = true;
        public string NomePropriedade { get; set; }
        public string Valor { get; set; }
        public TipoOperadorBusca Operador { get; set; }
    }

    public class FiltroBase : ICriterio
    {
        public FiltroBase()
        {
            AdicionarIncludes = true;
            OpcoesFiltro = new List<FiltroOpcao>();
        }

        public bool AdicionarIncludes { get; set; }
        public IList<FiltroOpcao> OpcoesFiltro { get; set; }
        public string Ordenacao { get; set; }

        public int Pagina { get; set; } = 1;
        public int ItensPorPagina { get; set; } = 20;
    }
}
