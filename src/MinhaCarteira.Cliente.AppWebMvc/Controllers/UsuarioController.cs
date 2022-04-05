using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;
using AutoMapper;
using MinhaCarteira.Cliente.Recursos.Refit;
using Microsoft.AspNetCore.Http;
using System;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Newtonsoft.Json;
using MinhaCarteira.Comum.Definicao.Modelo;
using System.Linq;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioServico _servico;
        private readonly IHttpContextAccessor _acessor;

        public UsuarioController(IMapper mapper, IUsuarioServico servico, IHttpContextAccessor acessor)
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
        public async Task<IActionResult> Logar(UsuarioLoginViewModel conta, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(conta);

            try
            {
                var itemMap = _mapper.Map<UsuarioLogin>(conta);
                var resposta = await _servico.Logar(itemMap);
                var userDb = _mapper.Map<UsuarioTokenViewModel>(resposta.Dados);

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, userDb.Nome),
                    new(ClaimTypes.Surname, userDb.Sobrenome),
                    new(ClaimTypes.NameIdentifier, userDb.Username),
                    new("FullName", userDb.NomeCompleto ?? userDb.Username),
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                claimsIdentity.AddClaims(
                    userDb.Roles
                        .Select(s => new Claim(ClaimTypes.Role, s))
                        .ToArray());

                claimsIdentity.AddClaims(
                    userDb.Preferences
                        .Select(s => new Claim(s.Key, s.Value))
                        .ToArray());

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

                return View(new UsuarioLoginViewModel());
            }
            catch (Exception e)
            {
                var retornoApi = new Resposta<Exception>(e, e.Message);
                ViewBag.RetornoApi = retornoApi;

                return View(new UsuarioLoginViewModel());
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

        [HttpPost]
        public async Task<IActionResult> GravarCondicaoSidebar(string valor)
        {
            var retorno = await Task.FromResult(User.Identity.IsAuthenticated);
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                var usuario = identity
                    .FindFirst(ClaimTypes.NameIdentifier)
                    .Value;

                var condicaoClaim = identity
                    .FindFirst("CondicaoSidebar");

                if (condicaoClaim != null)
                    identity.RemoveClaim(condicaoClaim);


                //identity.AddClaim(new Claim("CondicaoSidebar", valor));

                //User.Claims.SingleOrDefault(w => w.Type == chave).Value = valor;

                //var resposta = await _servico.ArmazenarPreferenciaUsuario(usuario, preferencia);
                retorno = true;

            }

            return Ok(retorno);
        }
    }
}
