using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.Recursos.Model;

namespace MinhaCarteira.Servidor.Recursos.Servico
{
    public static class TokenServico
    {
        public static string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuracao.Segredo);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static Resposta<UserToken> BuildToken(UserInfo usuario)
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

            return new Resposta<UserToken>(new UserToken()
            {
                TokenAcesso = new JwtSecurityTokenHandler().WriteToken(token),
                Username = usuario.Username,
                Expiration = expiration
            });
        }
    }
}
