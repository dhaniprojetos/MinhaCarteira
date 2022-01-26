using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Teste;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class ContaBancariaBuilder : IBuilder<ContaBancaria>
    {
        private readonly IBuilder<InstituicaoFinanceira> _financeiraBuilder;

        public ContaBancariaBuilder(IBuilder<InstituicaoFinanceira> financeiraBuilder)
        {
            _financeiraBuilder = financeiraBuilder;
        }

        public ContaBancaria DadosParaInsercao(params object[] args)
        {
            var instituicao = _financeiraBuilder.DadosParaInsercao(args);

            var faker = new Faker<ContaBancaria>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Finance.AccountName())
                .RuleFor(p => p.Agencia, f => f.Lorem.Word())
                .RuleFor(p => p.Conta, f => f.Finance.Account())
                .RuleFor(p => p.InstituicaoFinanceira, instituicao);

            var retorno = faker.Generate();
            return retorno;
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
