using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.Recursos.Servico;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(Usuario usuario)
        {
            if (!(usuario.Username == "hempmax" && usuario.Password == "maxhemp"))
            {
                var excecao = new Exception("Usuário ou senha inválidos.");
                return NotFound(new Resposta<Exception>(excecao, excecao.Message)
                {
                    StatusCode = 404
                });
            }

            usuario.Role = "Admin";
            usuario.Password = string.Empty;
            usuario.TokenAcesso = TokenServico.GerarToken(usuario);

            return Ok(new Resposta<Usuario>(usuario));
        }
    }
}
