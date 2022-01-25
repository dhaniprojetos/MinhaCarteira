using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Recursos.Refit.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class ContaBancariaController
        : BaseController<ContaBancaria, ContaBancariaViewModel>
    {
        private readonly IServicoBase<InstituicaoFinanceira> _instituicaoFinanceiraServico;

        public ContaBancariaController(
            IServicoBase<ContaBancaria> servico,
            IMapper mapper, 
            IServicoBase<InstituicaoFinanceira> instituicaoFinanceiraServico)
            : base(servico, mapper)
        {
            _instituicaoFinanceiraServico = instituicaoFinanceiraServico;
        }

        public override Task<IActionResult> Index()
        {
            return base.Index();
        }

        public override IActionResult Criar()
        {
            var bancos = _instituicaoFinanceiraServico.Navegar().Result;
            var viewModel = new ContaBancariaViewModel(bancos);

            return View(viewModel);
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
