using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UsuarioLogin usuario)
        {
            return Ok(new UsuarioToken());

            //if (!(usuario.Username == "hempmax" && usuario.PasswordHash == "maxhemp"))
            //{
            //    var excecao = new Exception("Usuário ou senha inválidos.");
            //    return NotFound(new Resposta<Exception>(excecao, excecao.Message)
            //    {
            //        StatusCode = 404
            //    });
            //}
            //
            ////usuario.Role = "Admin";
            //usuario.PasswordHash = string.Empty;
            ////usuario.TokenAcesso = TokenServico.GerarToken(usuario);
            //
            //return Ok(new Resposta<Usuario>(usuario));
        }
    }
}
