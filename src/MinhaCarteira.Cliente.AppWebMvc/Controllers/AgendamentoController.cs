using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using MinhaCarteira.Cliente.Recursos.Refit;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class AgendamentoController : BaseController<Agendamento, AgendamentoViewModel>
    {
        public AgendamentoController(
            IAgendamentoServico servico,
            IMapper mapper)
            : base(servico, mapper) { }

        protected override async Task<AgendamentoViewModel> InicializarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(viewModel);
        }
        protected override async Task<bool> ValidarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(true);
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index()
        {
            try
            {
                var resposta = await ((IAgendamentoServico)Servico).ContasAVencer(30);
                var itens = Mapper.Map<List<AgendamentoItemViewModel>>(resposta.Dados);
                
                if (!TempData.ContainsKey("RetornoApi")) return View(itens);
                var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);

                return View(itens);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                if (!TempData.ContainsKey("RetornoApi"))
                    ViewBag.RetornoApi = retornoApi;
                else
                {
                    var retorno = TempData["RetornoApi"].ToString() ?? string.Empty;
                    ViewBag.RetornoApi = JsonConvert.DeserializeObject<Resposta<object>>(retorno);
                }

                return View(new List<AgendamentoItemViewModel>());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new List<AgendamentoItemViewModel>());
            }
        }

        public override async Task<IActionResult> Criar()
        {
            return await base.Criar();
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
        #endregion
    }
}
