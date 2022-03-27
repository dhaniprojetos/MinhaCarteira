using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public ContaController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "<< Controlador ContaController :: MinhaCarteira.Servidor.WebApi >>";
        }

        [HttpPost]
        public async Task<ActionResult<Resposta<UserToken>>> Registrar([FromBody] UserInfo model)
        {
            var user = new IdentityUser()
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

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
                var resposta = TokenServico.BuildToken(model);
                resposta.Dados.Roles = roles;
                return Ok(resposta);
            }
            else
                return BadRequest(new Resposta<Exception>(e, e.Message));
        }
    }
}
