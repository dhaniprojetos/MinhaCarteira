using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;
using MinhaCarteira.Servidor.Modelo.Repositorio;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class ContaBancariaServico : ServicoBase<ContaBancaria>, IContaBancariaServico
    {
        public ContaBancariaServico(ICrud<ContaBancaria> repositorio) : base(repositorio)
        {
        }

        public async Task<bool> AtualizarSaldoConta(string id)
        {
            var retorno = await ((ContaBancariaRepositorio)Repositorio)
                .AtualizarSaldoConta(id);

            return retorno;
        }
    }
}
