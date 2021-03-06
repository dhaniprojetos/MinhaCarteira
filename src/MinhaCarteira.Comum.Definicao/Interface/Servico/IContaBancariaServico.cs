using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IContaBancariaServico 
        : IServicoCrud<ContaBancaria, IContaBancariaRepositorio>
    {
        Task<bool> AtualizarSaldoConta(string id);
    }
}
