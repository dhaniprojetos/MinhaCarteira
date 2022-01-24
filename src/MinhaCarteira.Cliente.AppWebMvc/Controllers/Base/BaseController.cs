using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Servico.Base;
using MinhaCarteira.Cliente.AppWebMvc.ViewModel;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers.Base
{
    public class BaseController<TEntidade, TEntidadeViewModel> : Controller
        where TEntidade : class, IEntidade
        where TEntidadeViewModel : class, IEntidade
    {
        private readonly IMapper _mapper;
        private readonly IServicoBase<TEntidade> _servico;

        private async Task<TEntidadeViewModel> ObterPorId(int id)
        {
            var resposta = await _servico.ObterPorId(id);
            TEntidadeViewModel item = default;

            if (resposta.Dados != null)
                item = _mapper.Map<TEntidadeViewModel>(resposta.Dados);

            return item;
        }

        public BaseController(IServicoBase<TEntidade> servico, IMapper mapper)
        {
            _mapper = mapper;
            _servico = servico;
        }

        public virtual async Task<IActionResult> Index()
        {
            var resposta = await _servico.Navegar();
            IList<TEntidadeViewModel> itens = null;

            if (resposta.Dados != null)
                itens = _mapper.Map<List<TEntidadeViewModel>>(resposta.Dados);

            return View(itens);
        }

        public virtual IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public virtual async Task<IActionResult> Criar(TEntidadeViewModel item)
        {
            if (ModelState.IsValid)
            {
                var itemArray = new[] { _mapper.Map<TEntidade>(item) };
                var itemDb = await _servico.Incluir(itemArray);
                //TempData["Adicionado"] = pessoaDb;

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public virtual async Task<IActionResult> Detalhes(int id)
        {
            var item = await ObterPorId(id);

            return View(item);
        }

        public virtual async Task<IActionResult> Alterar(int id)
        {
            var item = await ObterPorId(id);

            return View(item);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Alterar(TEntidadeViewModel item)
        {
            if (ModelState.IsValid)
            {
                var itemArray = new[] { _mapper.Map<TEntidade>(item) };
                var itemDb = await _servico.Alterar(itemArray);
                //TempData["Adicionado"] = pessoaDb;

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public virtual async Task<IActionResult> Deletar(int id)
        {
            var item = await ObterPorId(id);

            return View(item);
        }
        [HttpPost]
        public virtual async Task<IActionResult> Deletar(TEntidadeViewModel item)
        {
            if (ModelState.IsValid)
            {
                var itensId = await _servico.Deletar(item.Id);
                //TempData["Adicionado"] = pessoaDb;

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

    }
}
