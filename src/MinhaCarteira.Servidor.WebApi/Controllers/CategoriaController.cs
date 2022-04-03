using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class CategoriaController : 
        BaseController<Categoria, ICategoriaServico, ICategoriaRepositorio>
    {
        public CategoriaController(ICategoriaServico servico) 
            : base(servico)
        {
        }
    }
}
