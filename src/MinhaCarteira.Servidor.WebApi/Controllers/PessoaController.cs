using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class PessoaController : BaseController<Pessoa>
    {
        public PessoaController(IServicoCrud<Pessoa> servico) :
            base(servico) { }
    }
}
