﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Servidor.Recursos.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;
using System.Threading.Tasks;
using MinhaCarteira.Servidor.Modelo.Helper;

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

        [AllowAnonymous]
        [HttpPost]
        public override async Task<IActionResult> Incluir(Usuario item)
        {
            item.PasswordHash = item.PasswordHash.GerarHashSenha();

            return await base.Incluir(item);
        }

        [Route("armazenar-preferencia-usuario")]
        [HttpPost]
        public async Task<IActionResult> ArmazenarPreferenciaUsuario(string username, string chaveValor)
        {
            var retorno = await Servico
                .ArmazenarPreferenciaUsuario(username, chaveValor);

            return !retorno.BemSucedido
                ? NotFound(retorno)
                : Ok(retorno);
        }

    }
}
