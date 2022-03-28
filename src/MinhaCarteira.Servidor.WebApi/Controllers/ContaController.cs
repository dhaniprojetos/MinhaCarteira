using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using MinhaCarteira.Comum.Definicao.Helper;
using MinhaCarteira.Servidor.Recursos.Servico;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        /*
        private readonly UserManager<Usuario> _userManager;

        public ContaController(
            UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "<< Controlador ContaController :: MinhaCarteira.Servidor.WebApi >>";
        }

        [HttpPost]
        public async Task<ActionResult<Resposta<UserToken>>> Registrar([FromBody] Usuario model)
        {
            var result = await _userManager.CreateAsync(model, model.SenhaHash);

            return result.Succeeded
                ? TokenServico.BuildToken(model)
                : BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Resposta<UserToken>>> Login([FromBody] UserInfo model)
        {
            var e = new Exception("Login inválido.");

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return BadRequest(new Resposta<Exception>(e, e.Message));

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            var roles = await _userManager.GetRolesAsync(user);

            if (result)
            {
                var resposta = TokenServico.BuildToken(user);
                resposta.Dados.Roles = roles;
                return Ok(resposta);
            }
            else
                return BadRequest(new Resposta<Exception>(e, e.Message));
        }
        */

        private readonly IServicoCrud<Usuario> _servico;
        public ContaController(IServicoCrud<Usuario> servico)
        {
            _servico = servico;
        }

        private async Task<Resposta<LoginToken>> ObterPorUsuario(LoginUsuario model)
        {
            var filtroUsuario = new FiltroOpcao(
                nameof(model.Username),
                TipoOperadorBusca.Igual,
                model.Username);

            var filtroSenha = new FiltroOpcao(
                nameof(model.Senha),
                TipoOperadorBusca.Igual,
                model.Senha);

            var criterio = new FiltroBase()
            {
                OpcoesFiltro = new List<FiltroOpcao> {
                    filtroUsuario,
                    filtroSenha
                }
            };

            var usuarios = await _servico.Navegar(criterio);
            var user = usuarios.Item2.FirstOrDefault();
            Resposta<LoginToken> userToken = user == null
                ? new Resposta<LoginToken>(null, "Usuário não localizado.")
                {
                    StatusCode = 404
                }
                : TokenServico.BuildToken(user);

            return userToken;
        }
        private async Task<Resposta<LoginToken>> ObterPorEmail(LoginEmail model)
        {
            var filtroEmail = new FiltroOpcao(
                nameof(model.Email),
                TipoOperadorBusca.Igual,
                model.Email);

            var filtroSenha = new FiltroOpcao(
                nameof(model.Senha),
                TipoOperadorBusca.Igual,
                model.Senha);

            var criterio = new FiltroBase()
            {
                OpcoesFiltro = new List<FiltroOpcao> {
                    filtroEmail,
                    filtroSenha
                }
            };

            var usuarios = await _servico.Navegar(criterio);
            var user = usuarios.Item2.FirstOrDefault();
            Resposta<LoginToken> userToken = user == null
                ? new Resposta<LoginToken>(null, "Usuário não localizado.")
                {
                    StatusCode = 404
                }
                : TokenServico.BuildToken(user);

            return userToken;
        }

        [HttpPost]
        [Route("loginPorUsuario")]
        public async Task<IActionResult> LoginPorUsuario([FromBody] LoginUsuario model)
        {
            IActionResult resposta;
            try
            {
                var token = await ObterPorUsuario(model);

                resposta = token == null
                    ? NotFound(token)
                    : Ok(token);
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            return resposta;
        }

        [HttpPost]
        [Route("loginPorEmail")]
        public async Task<IActionResult> LoginPorEmail([FromBody] LoginEmail model)
        {
            IActionResult resposta;
            try
            {
                var token = await ObterPorEmail(model);

                resposta = token == null
                    ? NotFound(token)
                    : Ok(token);
            }
            catch (Exception e)
            {
                resposta = BadRequest(new Resposta<Exception>(e, e.Message));
            }

            return resposta;
        }
    }
}
