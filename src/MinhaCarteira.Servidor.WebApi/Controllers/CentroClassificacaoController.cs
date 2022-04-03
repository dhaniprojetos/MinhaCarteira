using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class CentroClassificacaoController : 
        BaseController<CentroClassificacao, ICentroClassificacaoServico>
    {
        public CentroClassificacaoController(ICentroClassificacaoServico servico)
            : base(servico) { }
    }
}
