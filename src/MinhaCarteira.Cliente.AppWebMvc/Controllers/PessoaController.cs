using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Servico;
using MinhaCarteira.Cliente.AppWebMvc.ViewModel;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPessoaServico _servico;

        public PessoaController(IPessoaServico servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var itens = await _servico.Navegar();

            return View(itens);
        }

        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Criar(PessoaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var pessoa = _mapper.Map<Pessoa>(item);
                var pessoaDb = await _servico.Incluir(new[] { pessoa });
                //TempData["Adicionado"] = pessoaDb;

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var item = await _servico.ObterPorId(id);

            return View(item);
        }

        public async Task<IActionResult> Alterar(int id)
        {
            var item = await _servico.ObterPorId(id);

            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Alterar(PessoaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var pessoa = _mapper.Map<Pessoa>(item);
                var pessoaDb = await _servico.Alterar(new[] { pessoa });
                //TempData["Adicionado"] = pessoaDb;

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            var item = await _servico.ObterPorId(id);

            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Deletar(PessoaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var pessoaDb = await _servico.Deletar(item.Id);
                //TempData["Adicionado"] = pessoaDb;

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }
    }
}
