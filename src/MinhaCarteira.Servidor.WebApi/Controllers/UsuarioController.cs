using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.Modelo.Helper;
using MinhaCarteira.Servidor.Recursos.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class UsuarioController :
        BaseController<Usuario, IUsuarioServico, IUsuarioRepositorio>
    {
        public UsuarioController(IUsuarioServico servico) : base(servico)
        {
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLogin userInfo)
        {
            var retorno = await Servico.Login(userInfo);

            if (retorno.BemSucedido)
                retorno.Dados.TokenAcesso = 
                    TokenServico.GerarToken(userInfo);

            return !retorno.BemSucedido
                ? NotFound(retorno)
                : Ok(retorno);
        }
    }
}
