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
        private readonly SignInManager<IdentityUser> _signInManager;

        public ContaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            //var result = await _signInManager.PasswordSignInAsync(
            //    userName: model.Usuario,
            //    password: model.Password,
            //    isPersistent: false,
            //    lockoutOnFailure: false);

            var user = await _userManager.FindByNameAsync(model.Username);
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            var roles = await _userManager.GetRolesAsync(user);

            if (result)
            {
                var resposta = TokenServico.BuildToken(model);
                resposta.Dados.Roles = "Admin";
                return Ok(resposta);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido.");
                return BadRequest(ModelState);
            }
        }

        //[Route("login")]
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Login(Usuario usuario)
        //{
        //    if (!(usuario.Username == "hempmax" && usuario.Password == "maxhemp"))
        //    {
        //        var excecao = new Exception("Usuário ou senha inválidos.");
        //        return NotFound(new Resposta<Exception>(excecao, excecao.Message)
        //        {
        //            StatusCode = 404
        //        });
        //    }
        //
        //    usuario.Role = "Admin";
        //    usuario.Password = string.Empty;
        //    usuario.TokenAcesso = TokenServico.GerarToken(usuario);
        //
        //    return Ok(new Resposta<Usuario>(usuario));
        //}
    }
}
