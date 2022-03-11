using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Cliente.Recursos.Models.Base;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers.Base
{
    [Authorize(Roles = "Admin")]
    public abstract class BaseController<TEntidade, TEntidadeViewModel> : Controller
        where TEntidade : class, IEntidade
        where TEntidadeViewModel : BaseViewModel, IEntidade
    {
        private readonly IMapper _mapper;
        protected IMapper Mapper => _mapper;
        private readonly IServicoBase<TEntidade> _servico;
        protected IServicoBase<TEntidade> Servico => _servico;

        public BaseController(IServicoBase<TEntidade> servico, IMapper mapper)
        {
            _mapper = mapper;
            _servico = servico;
        }

        protected virtual async Task<TEntidadeViewModel> ObterPorId(int id)
        {
            try
            {
                var resposta = await _servico.ObterPorId(id);
                TEntidadeViewModel item = default;

                if (resposta.Dados != null)
                    item = _mapper.Map<TEntidadeViewModel>(resposta.Dados);

                return item;
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
                return null;
            }
        }
        protected virtual async Task<Tuple<int, IList<TEntidadeViewModel>>> ObterTodos(ICriterio criterio)
        {
            var resposta = await _servico.Navegar(criterio);
            var itens = _mapper.Map<List<TEntidadeViewModel>>(resposta.Dados);
            return new Tuple<int, IList<TEntidadeViewModel>>(
                resposta.TotalRegistros,
                itens);
        }
        protected virtual async Task<Tuple<TEntidadeViewModel, TEntidade>> ExecutarAntesSalvar(
            TEntidadeViewModel viewModel, TEntidade model)
        {
            return await Task.FromResult(
                new Tuple<TEntidadeViewModel, TEntidade>(viewModel, model));
        }
        protected abstract Task<TEntidadeViewModel> InicializarViewModel(
            TEntidadeViewModel viewModel);
        protected abstract Task<bool> ValidarViewModel(TEntidadeViewModel viewModel);

        public virtual async Task<IActionResult> Detalhes(int id)
        {
            var item = await ObterPorId(id);

            return View(item);
        }

        public virtual async Task<IActionResult> Index(int? page, ListaBaseViewModel<TEntidadeViewModel> model)
        {
            try
            {
                var criterio = new FiltroBase()
                {
                    Pagina = page ?? 1
                };

                var opcao = model.OpcaoAtual;
                if (!string.IsNullOrWhiteSpace(opcao?.NomePropriedade) &&
                    !string.IsNullOrWhiteSpace(opcao?.Valor))
                    criterio.OpcoesFiltro.Add(opcao);

                var itens = await ObterTodos(criterio);
                var itensPaginados = new ListaBaseViewModel<TEntidadeViewModel>(
                    itens.Item2, criterio, itens.Item1);

                if (!TempData.ContainsKey("RetornoApi")) return View(itensPaginados);
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return View(itensPaginados);
            }
            catch (Refit.ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Logar", "Conta");

                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                if (retornoApi == null)
                    retornoApi = new Resposta<Exception>(ex, ex.Message)
                    {
                        StatusCode = (int)ex.StatusCode
                    };

                if (!TempData.ContainsKey("RetornoApi"))
                    ViewBag.RetornoApi = retornoApi;
                else
                {
                    var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                    ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);
                }

                return View(new ListaBaseViewModel<TEntidadeViewModel>());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new ListaBaseViewModel<TEntidadeViewModel>());
            }
        }

        public virtual async Task<IActionResult> Criar()
        {
            try
            {
                var item = await InicializarViewModel(
                    Activator.CreateInstance<TEntidadeViewModel>());

                return View(item);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(Activator.CreateInstance<TEntidadeViewModel>());
            }
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
                var items = await ExecutarAntesSalvar(item, itemApi);
                var retornoApi = await _servico.Incluir(items.Item2);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
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
                var items = await ExecutarAntesSalvar(item, itemApi);
                var retornoApi = await _servico.Alterar(items.Item2);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Super, Admin")]
        public virtual async Task<IActionResult> Deletar(int id)
        {
            var item = await ObterPorId(id);
            item = await InicializarViewModel(item);

            return View(item);
        }
        [HttpPost]
        [Authorize(Roles = "Super, Admin")]
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
            catch (Refit.ApiException ex)
            {
                //var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = ex.Content;
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
