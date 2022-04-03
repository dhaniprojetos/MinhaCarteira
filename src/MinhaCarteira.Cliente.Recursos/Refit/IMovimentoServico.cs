using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    public interface IMovimentoServico : IServicoBase<MovimentoBancario>
    {
        [Get("/obter-movimentos-para-conciliacao")]
        Task<RespostaPaginada<IList<MovimentoBancario>>> ObterMovimentosParaConciliacao([Body] ICriterio criterio);
    }
}
