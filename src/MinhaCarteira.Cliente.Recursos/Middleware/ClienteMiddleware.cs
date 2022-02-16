using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

            //var  = new 
            //{
            //    AuthorizationHeaderValueGetter = async () =>
            //        await Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImhlbXBtYXgiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2NDQ5NTA1NjMsImV4cCI6MTY0NDk1NDE2MywiaWF0IjoxNjQ0OTUwNTYzfQ.jEogrlEFNVKslcUsrheXtkIGVs_mUK-e8EIyN0ox_DA")
            //};
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<AuthorizationMessageHandler>();

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
                .AddRefitClient<IServicoBase<ContaBancaria>>()
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
                .AddRefitClient<IServicoBase<MovimentoBancario>>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/movimentobancario"));

            services
                .AddRefitClient<IServicoBase<Agendamento>>()
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/agendamento"));

            return services;
        }
    }
}