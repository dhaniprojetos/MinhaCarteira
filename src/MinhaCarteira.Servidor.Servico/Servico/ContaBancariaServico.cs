using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class ContaBancariaServico 
        : ServicoBase<ContaBancaria, IContaBancariaRepositorio>, IContaBancariaServico
    {
        public ContaBancariaServico(IContaBancariaRepositorio repositorio) : base(repositorio)
        {
        }

        public async Task<bool> AtualizarSaldoConta(string id)
        {
            var retorno = await Repositorio.AtualizarSaldoConta(id);

            return retorno;
        }
    }
}
