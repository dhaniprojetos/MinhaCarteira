using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    public interface IContaBancariaServico : IServicoBase<ContaBancaria, ICriterio<ContaBancaria>>
    {
        [Post("/atualizar-saldo-conta")]
        Task<Resposta<bool>> AtualizarSaldoConta(string idsContaBancaria);
    }
}
