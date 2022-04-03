using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface IContaBancariaRepositorio 
        : ICrud<ContaBancaria>
    {
        Task<bool> AtualizarSaldoConta(string id);
    }
}
