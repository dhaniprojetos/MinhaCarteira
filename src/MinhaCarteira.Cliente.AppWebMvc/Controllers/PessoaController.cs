using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Servico.Base;
using MinhaCarteira.Cliente.AppWebMvc.ViewModel;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class PessoaController : BaseController<Pessoa, PessoaViewModel>
    {
        public PessoaController(IServicoBase<Pessoa> servico, IMapper mapper)
            : base(servico, mapper) { }

        public override Task<IActionResult> Index()
        {
            return base.Index();
        }

        public override IActionResult Criar()
        {
            return base.Criar();
        }

        public override async Task<IActionResult> Detalhes(int id)
        {
            return await base.Detalhes(id);
        }

        public override async Task<IActionResult> Alterar(int id)
        {
            return await base.Alterar(id);
        }

        public override async Task<IActionResult> Deletar(int id)
        {
            return await base.Deletar(id);
        }
    }
}
