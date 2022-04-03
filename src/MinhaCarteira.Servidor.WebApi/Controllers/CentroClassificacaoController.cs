using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class CentroClassificacaoController : 
        BaseController<CentroClassificacao, ICentroClassificacaoServico, ICentroClassificacaoRepositorio>
    {
        public CentroClassificacaoController(ICentroClassificacaoServico servico)
            : base(servico) { }
    }
}
