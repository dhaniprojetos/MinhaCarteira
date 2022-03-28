using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.Recursos.Model;

namespace MinhaCarteira.Servidor.Recursos.Servico
{
    public static class TokenServico
    {
        public static Resposta<LoginToken> BuildToken(Usuario usuario)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuracao.Segredo));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //tempo de expiração do token: 7 dias
            var expiration = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var resposta = new Resposta<LoginToken>(new LoginToken()
            {
                TokenAcesso = new JwtSecurityTokenHandler().WriteToken(token),
                Username = usuario.Username,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Expiration = expiration
            }, "Usuário localizado com sucesso.")
            {
                StatusCode = 200
            };

            return resposta;
        }
    }
}
