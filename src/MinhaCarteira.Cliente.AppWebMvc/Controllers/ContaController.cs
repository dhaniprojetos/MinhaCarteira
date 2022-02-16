using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Models;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class ContaController : Controller
    {


        public IActionResult Logar(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(ContaViewModel conta, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            //if (ModelState.IsValid && conta.Usuario == "hempmax" && conta.Senha == "maxhemp")
            if (ModelState.IsValid)
            {


                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, conta.Usuario),
                    new("FullName", conta.Usuario),
                    new(ClaimTypes.Role, "Admin"),
                };
                
                var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(claimsIdentity),
                    null);
                
                return Url.IsLocalUrl(returnUrl)
                    ? Redirect(returnUrl)
                    : RedirectToAction("Index", "Home");

                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(conta);

        }
    }
}
