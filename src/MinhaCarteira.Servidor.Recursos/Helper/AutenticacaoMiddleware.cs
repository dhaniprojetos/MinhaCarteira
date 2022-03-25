using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MinhaCarteira.Servidor.Recursos.Model;

namespace MinhaCarteira.Servidor.Recursos.Helper
{
    public static class AutenticacaoMiddleware
    {
        public static IServiceCollection AdicionarAutenticacao(
            this IServiceCollection services)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuracao.Segredo));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = System.TimeSpan.Zero
                };
            });

            services.AddCors();

            return services;
        }
    }
}
