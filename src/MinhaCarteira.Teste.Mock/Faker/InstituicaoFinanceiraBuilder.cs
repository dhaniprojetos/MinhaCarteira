using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class InstituicaoFinanceiraBuilder : IBuilder<InstituicaoFinanceira>
    {
        public Faker<InstituicaoFinanceira> DadosParaInsercao(params object[] args)
        {
            var faker = new Faker<InstituicaoFinanceira>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Company.CompanyName());

            return faker;
        }

        public InstituicaoFinanceira DadosParaAlteracao(InstituicaoFinanceira item)
        {
            item.Nome += " alterado";

            return item;
        }
    }
}
