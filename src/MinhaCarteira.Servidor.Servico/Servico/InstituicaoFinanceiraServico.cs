using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class InstituicaoFinanceiraServico : ServicoBase<InstituicaoFinanceira>
    {
        public InstituicaoFinanceiraServico(ICrud<InstituicaoFinanceira> repositorio) : base(repositorio)
        {

        }
    }
}
