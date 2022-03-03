using System.Collections.Generic;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Filtro
{
    public class FiltroMovimentoBancario : ICriterio<MovimentoBancario>
    {
        public FiltroMovimentoBancario()
        {
            AdicionarIncludes = true;
            Filtros = new List<KeyValuePair<string, string>>();
        }

        public void AdicionarFiltro(KeyValuePair<string, string> opcao)
        {
            Filtros.Add(opcao);
        }

        public bool AdicionarIncludes { get; set; }
        public IList<KeyValuePair<string, string>> Filtros { get; set; }
    }
}
