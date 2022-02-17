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

            var faker = new Faker<ContaBancaria>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Finance.AccountName())
                .RuleFor(p => p.Agencia, f => f.Lorem.Word())
                .RuleFor(p => p.Conta, f => f.Finance.Account())
                .RuleFor(p => p.InstituicaoFinanceira, instituicao.Generate());

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
