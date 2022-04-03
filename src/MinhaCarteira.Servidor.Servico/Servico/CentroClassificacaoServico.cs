using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class CentroClassificacaoServico 
        : ServicoBase<CentroClassificacao, ICentroClassificacaoRepositorio>,
          ICentroClassificacaoServico
    {
        public CentroClassificacaoServico(ICentroClassificacaoRepositorio repositorio) : base(repositorio)
        {
        }
    }
}
