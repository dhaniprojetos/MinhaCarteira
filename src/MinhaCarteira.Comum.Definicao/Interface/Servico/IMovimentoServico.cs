using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IMovimentoServico : IServicoCrud<MovimentoBancario>
    {
        Task<IList<MovimentoBancario>> ObterMovimentosParaConciliacao();
    }
}
