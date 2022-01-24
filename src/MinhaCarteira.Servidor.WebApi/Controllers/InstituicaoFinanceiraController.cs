using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class InstituicaoFinanceiraController : BaseController<InstituicaoFinanceira>
    {
        public InstituicaoFinanceiraController(IServicoCrud<InstituicaoFinanceira> servico) : base(servico)
        {
        }
    }
}
