using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class PessoaController : 
        BaseController<Pessoa, IPessoaServico, IPessoaRepositorio>
    {
        public PessoaController(IPessoaServico servico)
            : base(servico)
        {
        }
    }
}