using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class MovimentoBancarioController : BaseController<MovimentoBancario, MovimentoBancarioViewModel>
    {
        private readonly IServicoBase<Pessoa> _pessoaServico;
        private readonly IServicoBase<CentroClassificacao> _centroClassificacaoServico;
        private readonly IServicoBase<Categoria> _categoriaServico;
        private readonly IContaBancariaServico _contaBancariaServico;

        public MovimentoBancarioController(
            IMovimentoServico servico,
            IMapper mapper,
            IServicoBase<Pessoa> pessoaServico,
            IServicoBase<CentroClassificacao> centroClassificacaoServico,
            IServicoBase<Categoria> categoriaServico,
            IContaBancariaServico contaBancariaServico)
            : base(servico, mapper)
        {
            _pessoaServico = pessoaServico;
            _centroClassificacaoServico = centroClassificacaoServico;
            _categoriaServico = categoriaServico;
            _contaBancariaServico = contaBancariaServico;
        }

        protected override async Task<MovimentoBancarioViewModel> InicializarViewModel(MovimentoBancarioViewModel viewModel)
        {
            var respPessoa = await _pessoaServico.Navegar(null);
            viewModel.AdicionarPessoas(respPessoa.Dados);

            var respCentros = await _centroClassificacaoServico.Navegar(null);
            viewModel.AdicionarCentrosClassificacao(respCentros.Dados);

            var respCategorias = await _categoriaServico.Navegar(null);
            viewModel.AdicionarCategorias(respCategorias.Dados);

            var respContas = await _contaBancariaServico.Navegar(null);
            viewModel.AdicionarContasBancarias(respContas.Dados);

            return await Task.FromResult(viewModel);
        }

        protected override async Task<bool> ValidarViewModel(MovimentoBancarioViewModel viewModel)
        {
            return await Task.FromResult(true);
        }

        [HttpPost]
        public async Task<JsonResult> ObterContaBancaria(string prefix)
        {
            var resp = await _contaBancariaServico.Navegar(null);

            var items = resp.Dados
                .Select(s => new { label = s.Nome, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        [HttpPost]
        public async Task<JsonResult> ObterCategoria(string prefix)
        {
            var resp = await _categoriaServico.Navegar(null);

            var items = resp.Dados
                .Select(s => new { label = s.Caminho, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        [HttpPost]
        public async Task<JsonResult> ObterPessoa(string prefix)
        {
            var resp = await _pessoaServico.Navegar(null);

            var items = resp.Dados
                .Select(s => new { label = s.Nome, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        [HttpPost]
        public async Task<JsonResult> ObterCentroClassificacao(string prefix)
        {
            var resp = await _centroClassificacaoServico.Navegar(null);

            var items = resp.Dados
                .Select(s => new { label = s.Nome, val = s.Id })
                .Where(w => string.IsNullOrEmpty(prefix) ||
                            w.label.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(s => s.label)
                .ToList();

            return Json(items);
        }

        public async Task<IActionResult> Clonar(string id)
        {
            try
            {
                MovimentoBancarioViewModel item = await ObterPorId(int.Parse(id));

                return View(nameof(Criar), item);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(nameof(Criar), new MovimentoBancarioViewModel());
            }
        }

        private async Task<ListaMovimentoBancarioViewModel> ObterMovimentosConta(
            int idContaBancaria, FiltroBase<MovimentoBancario> filtroMovimento)
        {
            var retornoApi = await _contaBancariaServico.Navegar(null);
            var contas = Mapper.Map<IList<ContaBancariaViewModel>>(retornoApi.Dados);

            IList<MovimentoBancarioViewModel> movimentos;
            try
            {
                var retornoMovimentos = await Servico.Navegar(filtroMovimento);
                movimentos = Mapper.Map<IList<MovimentoBancarioViewModel>>(retornoMovimentos.Dados);
            }
            catch (Exception)
            {
                movimentos = new List<MovimentoBancarioViewModel>();
            }

            var item = new ListaMovimentoBancarioViewModel()
            {
                Contas = contas,
                Movimentos = movimentos
                    .Where(w => w.ContaBancariaId == idContaBancaria)
                    .ToList()
            };

            return item;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            try
            {
                var idConta = int.Parse(id ?? "1");
                var filtroMovimento = new FiltroBase<MovimentoBancario>()
                {
                    OpcoesFiltro = {
                        new FiltroOpcao("ContaBancariaId", TipoOperadorBusca.Igual, idConta),
                        //new FiltroOpcao("DataMovimento"  , TipoOperadorBusca.Maior, DateTime.Now.AddDays(-20))
                    }
                };

                var item = await ObterMovimentosConta(idConta, filtroMovimento);

                return View(item);
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new ListaMovimentoBancarioViewModel());
            }
        }

        //[Route("criar")]
        [HttpGet]
        public async Task<IActionResult> Criar(string idContaBancaria)
        {
            var idConta = int.Parse(idContaBancaria ?? "1");
            var retornoApi = await _contaBancariaServico.ObterPorId(idConta);
            var conta = Mapper.Map<ContaBancariaViewModel>(retornoApi.Dados);

            var item = await InicializarViewModel(new MovimentoBancarioViewModel());
            item.ContaBancaria = conta;
            item.ContaBancariaId = conta.Id;

            return View(item);
        }

        [HttpPost]
        public override async Task<IActionResult> Criar(MovimentoBancarioViewModel item)
        {
            var retorno = await base.Criar(item);

            if (retorno is RedirectToActionResult)
                return RedirectToAction(nameof(Index), new { id = item.ContaBancariaId });

            return retorno;
        }

        [HttpPost]
        public async override Task<IActionResult> Alterar(MovimentoBancarioViewModel item)
        {
            var retorno = await base.Alterar(item);

            if (retorno is RedirectToActionResult)
                return RedirectToAction(nameof(Index), new { id = item.ContaBancariaId });

            return retorno;
        }

        [HttpPost]
        public async override Task<IActionResult> Deletar(MovimentoBancarioViewModel item)
        {
            var retorno = await base.Deletar(item);

            if (retorno is RedirectToActionResult)
                return RedirectToAction(nameof(Index), new { id = item.ContaBancariaId });

            return retorno;
        }

        #region Métodos sobrescritos apenas manter as views
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
        #endregion
    }
}