using System;
using Refit;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Recursos.Refit.Base;

namespace MinhaCarteira.Comum.Recursos.Helper
{
    public static class ClienteMiddleware
    {
        public static IServiceCollection AdicionarConexoesRefit(
            this IServiceCollection services, string baseUrlApi)
        {
            if (baseUrlApi.EndsWith('/'))
                baseUrlApi = baseUrlApi.Remove(baseUrlApi.Length - 1, 1);

            services
                .AddRefitClient<IServicoBase<Pessoa>>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/pessoa"));

            services
                .AddRefitClient<IServicoBase<InstituicaoFinanceira>>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/instituicaofinanceira"));

            services
                .AddRefitClient<IServicoBase<ContaBancaria>>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/contabancaria"));

            services
                .AddRefitClient<IServicoBase<Categoria>>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/categoria"));

            services
                .AddRefitClient<IServicoBase<CentroClassificacao>>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/centroclassificacao"));

            services
                .AddRefitClient<IServicoBase<MovimentoBancario>>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(baseUrlApi + "/movimentobancario"));

            return services;
        }
    }
}