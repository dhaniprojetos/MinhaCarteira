using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Cliente.Recursos.Refit;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Cliente.Recursos.Refit.Handler;
using MinhaCarteira.Comum.Definicao.Entidade;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Middleware
{
    public static class ClienteMiddleware
    {
        public static IServiceCollection AdicionarConexoesRefit(
            this IServiceCollection services, string baseUrlApi)
        {
            if (baseUrlApi.EndsWith('/'))
                baseUrlApi = baseUrlApi.Remove(baseUrlApi.Length - 1, 1);
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<AuthorizationMessageHandler>();

            services
                .AddRefitClient<IContaServico>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/conta"));

            services
                .AddRefitClient<IServicoBase<Pessoa>>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                        c.BaseAddress = new Uri(baseUrlApi + "/pessoa"));

            services
                .AddRefitClient<IServicoBase<InstituicaoFinanceira>>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/instituicaofinanceira"));

            services
                .AddRefitClient<IContaBancariaServico>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/contabancaria"));

            services
                .AddRefitClient<IServicoBase<Categoria>>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/categoria"));

            services
                .AddRefitClient<IServicoBase<CentroClassificacao>>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/centroclassificacao"));

            services
                .AddRefitClient<IMovimentoServico>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/movimentobancario"));

            services
                .AddRefitClient<IAgendamentoServico>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/agendamento"));

            return services;
        }
    }
}