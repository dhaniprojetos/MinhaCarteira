using System;
using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
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

        public override bool Equals(object obj)
        {
            var other = (FiltroOpcao)obj;
            return
                NomePropriedade.Equals(other.NomePropriedade, StringComparison.InvariantCultureIgnoreCase) &&
                Valor.Equals(other.Valor, StringComparison.InvariantCultureIgnoreCase) &&
                Operador.Equals(other.Operador) &&
                Visivel.Equals(other.Visivel);
        }
        public override int GetHashCode()

        {

            int IDHashCode = this.NomePropriedade.GetHashCode();
            int NameHashCode = this.Valor == null ? 0 : this.Valor.GetHashCode();

            return IDHashCode ^ NameHashCode;

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
