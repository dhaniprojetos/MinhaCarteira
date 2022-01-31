using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class AgendamentoController : BaseController<Agendamento>
    {
        public AgendamentoController(IServicoCrud<Agendamento> servico) : base(servico)
        {
        }
    }
}
