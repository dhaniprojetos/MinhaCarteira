using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class InstituicaoFinanceiraServico 
        : ServicoBase<InstituicaoFinanceira, IInstituicaoFinanceiraRepositorio>, IInstituicaoFinanceiraServico
    {
        public InstituicaoFinanceiraServico(IInstituicaoFinanceiraRepositorio repositorio) : base(repositorio)
        {

        }
    }
}
