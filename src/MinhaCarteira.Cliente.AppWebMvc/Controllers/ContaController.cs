using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;
using AutoMapper;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Comum.Definicao.Entidade;
using Microsoft.AspNetCore.Http;
using System;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class ContaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IContaServico _servico;
        private readonly IHttpContextAccessor _acessor;

        public ContaController(IMapper mapper, IContaServico servico, IHttpContextAccessor acessor)
        {
            _mapper = mapper;
            _servico = servico;
            _acessor = acessor;
        }

        public IActionResult Logar(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(UsuarioViewModel conta, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(conta);

            try
            {
                var itemMap = _mapper.Map<UserInfo>(conta);
                var resposta = await _servico.Logar(itemMap);
                var userDb = _mapper.Map<UsuarioViewModel>(resposta.Dados);

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, userDb.Username),
                    new("FullName", userDb.Username),
                    //new(ClaimTypes.Role, userDb.Roles)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                foreach (var role in userDb.Roles)
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await Request.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal);

                Response.Cookies.Append("Bearer", userDb.TokenAcesso);

                return Url.IsLocalUrl(returnUrl)
                    ? Redirect(returnUrl)
                    : RedirectToAction("Index", "Home");
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

                return View(new UsuarioViewModel());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new UsuarioViewModel());
            }

        }

        public async Task<IActionResult> Sair()
        {
            Response.Cookies.Delete("Bearer");
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}
