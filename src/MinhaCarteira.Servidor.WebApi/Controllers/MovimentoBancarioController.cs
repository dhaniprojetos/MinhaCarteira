using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class MovimentoBancarioController : BaseController<MovimentoBancario>
    {
        public MovimentoBancarioController(IServicoCrud<MovimentoBancario> servico)
            : base(servico) { }
    }
}
