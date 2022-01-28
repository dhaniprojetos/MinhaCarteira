using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;
using Newtonsoft.Json;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers.Base
{
    public abstract class BaseController<TEntidade, TEntidadeViewModel> : Controller
        where TEntidade : class, IEntidade
        where TEntidadeViewModel : class, IEntidade
    {
        private readonly IMapper _mapper;
        private readonly IServicoBase<TEntidade> _servico;

        protected IMapper Mapper => _mapper;
        protected IServicoBase<TEntidade> Servico => _servico;

        protected async Task<TEntidadeViewModel> ObterPorId(int id)
        {
            try
            {
                var resposta = await _servico.ObterPorId(id);
                TEntidadeViewModel item = default;

                if (resposta.Dados != null)
                    item = _mapper.Map<TEntidadeViewModel>(resposta.Dados);

                return item;
            }
            catch (ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = retornoApi;
                return null;
            }
        }

        public BaseController(IServicoBase<TEntidade> servico, IMapper mapper)
        {
            _mapper = mapper;
            _servico = servico;
        }

        protected abstract Task<TEntidadeViewModel> InicializarViewModel(
            TEntidadeViewModel viewModel);

        protected abstract Task<bool> ValidarViewModel(TEntidadeViewModel viewModel);

        public virtual async Task<IActionResult> Detalhes(int id)
        {
            var item = await ObterPorId(id);

            return View(item);
        }

        public virtual async Task<IActionResult> Index()
        {
            try
            {
                IList<TEntidadeViewModel> itens = null;
                var resposta = await _servico.Navegar();

                if (resposta.Dados != null)
                    itens = _mapper.Map<List<TEntidadeViewModel>>(resposta.Dados);

                if (!TempData.ContainsKey("RetornoApi")) return View(itens);
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return View(itens);
            }
            catch (ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = retornoApi;
                return View(default);
            }
        }

        public virtual async Task<IActionResult> Criar()
        {
            var item = await InicializarViewModel(
                Activator.CreateInstance<TEntidadeViewModel>());

            return await Task.Run(() => View(item));
        }
        [HttpPost]
        public virtual async Task<IActionResult> Criar(TEntidadeViewModel item)
        {
            if (!await ValidarViewModel(item) || !ModelState.IsValid)
            {
                item = await InicializarViewModel(item);
                return View(item);
            }

            try
            {
                var itemApi = _mapper.Map<TEntidade>(item);
                var retornoApi = await _servico.Incluir(itemApi);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = retornoApi;
            }

            return RedirectToAction(nameof(Index));

        }

        public virtual async Task<IActionResult> Alterar(int id)
        {
            var item = await ObterPorId(id);
            item = await InicializarViewModel(item);

            return View(item);
        }
        [HttpPost]
        public virtual async Task<IActionResult> Alterar(TEntidadeViewModel item)
        {
            if (!await ValidarViewModel(item) || !ModelState.IsValid)
            {
                item = await InicializarViewModel(item);
                return View(item);
            }

            try
            {
                var itemApi = _mapper.Map<TEntidade>(item);
                var retornoApi = await _servico.Alterar(itemApi);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = retornoApi;
            }

            return RedirectToAction(nameof(Index));
        }

        public virtual async Task<IActionResult> Deletar(int id)
        {
            var item = await ObterPorId(id);
            item = await InicializarViewModel(item);

            return View(item);
        }
        [HttpPost]
        public virtual async Task<IActionResult> Deletar(TEntidadeViewModel item)
        {
            if (!ModelState.IsValid)
            {
                item = await InicializarViewModel(item);
                return View(item);
            }

            try
            {
                var retornoApi = await _servico.Deletar(item.Id);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }

            return RedirectToAction(nameof(Index));

        }

    }
}
