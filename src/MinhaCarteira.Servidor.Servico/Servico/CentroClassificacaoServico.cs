using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class CentroClassificacaoServico : ServicoBase<CentroClassificacao>
    {
        public CentroClassificacaoServico(ICrud<CentroClassificacao> repositorio) : base(repositorio)
        {
        }
    }
}
