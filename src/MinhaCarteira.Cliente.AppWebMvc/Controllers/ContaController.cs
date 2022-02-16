using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;
using AutoMapper;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Comum.Definicao.Entidade;
using Microsoft.AspNetCore.Http;

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

            //if (ModelState.IsValid && conta.Usuario == "hempmax" && conta.Senha == "maxhemp")
            if (ModelState.IsValid)
            {
                var itemMap = _mapper.Map<Usuario>(conta);
                var resposta = await _servico.Logar(itemMap);
                var userDb = _mapper.Map<UsuarioViewModel>(resposta.Dados);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, userDb.Username),
                    new("FullName", userDb.Username),
                    new(ClaimTypes.Role, userDb.Role)
                };

                var cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(1)
                };

                Response.Cookies.Append("Bearer", userDb.TokenAcesso, cookieOptions);

                var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    null);

                return Url.IsLocalUrl(returnUrl)
                    ? Redirect(returnUrl)
                    : RedirectToAction("Index", "Home");

                //return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(conta);

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
