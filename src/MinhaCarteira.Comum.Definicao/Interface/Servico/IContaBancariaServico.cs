using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IContaBancariaServico : IServicoCrud<ContaBancaria>
    {
        Task<bool> AtualizarSaldoConta(string id);
    }
}
