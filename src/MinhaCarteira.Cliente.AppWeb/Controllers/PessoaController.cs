using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWeb.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWeb.Controllers
{
    [Route("[controller]/[action]")]
    public class PessoaController : Controller
    {
        //private readonly IPessoaServico _servico;
        //public PessoaController(IPessoaServico servico)
        //{
        //    _servico = servico;
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var itens = await _servico.Navegar(null);

            //return View(itens);
            return View();
        }
    }
}