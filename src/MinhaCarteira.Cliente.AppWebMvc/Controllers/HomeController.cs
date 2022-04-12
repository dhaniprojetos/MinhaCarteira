using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using MinhaCarteira.Cliente.Recursos.Models;
using Microsoft.AspNetCore.Http;
using MinhaCarteira.Cliente.Recursos.Refit;
using System.Threading.Tasks;
using System.Linq;
using System;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRelatorioServico _servico;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            IRelatorioServico servico)
        {
            _logger = logger;
            _servico = servico;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ObterExtratos()
        {
            try
            {
                var resp = await _servico.ObterExtratos();
                return Json(resp.Dados);
            }
            catch (Refit.ApiException ex)
            {
                var retornoApi = await ex.GetContentAsAsync<Resposta<Exception>>();
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
                
                return null;
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                TempData["RetornoApi"] = JsonConvert.SerializeObject(retornoApi);
                
                return null;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
