using System;
using System.Collections.Generic;
using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class ContaBancariaBuilder : IBuilder<ContaBancaria>
    {
        private readonly IBuilder<InstituicaoFinanceira> _financeiraBuilder;

        public ContaBancariaBuilder(IBuilder<InstituicaoFinanceira> financeiraBuilder)
        {
            _financeiraBuilder = financeiraBuilder;
        }

        public Faker<ContaBancaria> DadosParaInsercao(params object[] args)
        {
            var instituicao = _financeiraBuilder.DadosParaInsercao(args);

            IList<InstituicaoFinanceira> bancos = args != null && args[0] is IList<InstituicaoFinanceira> itens
                ? itens
                : new List<InstituicaoFinanceira>(instituicao.Generate(1));

            var now = DateTime.Now.AddMonths(-2);
            var ultimoDia = DateTime.DaysInMonth(now.Year, now.Month);
            var dataInicial = new DateTime(now.Year, now.Month, 1);
            var dataFinal = new DateTime(now.Year, now.Month, ultimoDia);

            var faker = new Faker<ContaBancaria>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Finance.AccountName())
                .RuleFor(p => p.Agencia, f => f.Lorem.Word())
                .RuleFor(p => p.Conta, f => f.Finance.Account())
                .RuleFor(p => p.InstituicaoFinanceira, f => f.PickRandom(bancos))
                .RuleFor(p => p.DataSaldoInicial, f => f.Date.Between(dataInicial, dataFinal))
                .RuleFor(p => p.ValorSaldoInicial, f => f.Random.Decimal(-999, 999));

            return faker;
        }

        public ContaBancaria DadosParaAlteracao(ContaBancaria item)
        {
            var instituicao = _financeiraBuilder.DadosParaInsercao();
            item.Nome += " alterado";
            item.InstituicaoFinanceira = instituicao;

            return item;
        }
    }
}
