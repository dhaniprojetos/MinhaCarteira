using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class ContaBancariaController : BaseController<ContaBancaria>
    {
        public ContaBancariaController(IServicoCrud<ContaBancaria> servico) : base(servico)
        {
        }
    }
}
