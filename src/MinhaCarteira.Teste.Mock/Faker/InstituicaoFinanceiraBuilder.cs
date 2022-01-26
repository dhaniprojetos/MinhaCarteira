using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Teste;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class InstituicaoFinanceiraBuilder : IBuilder<InstituicaoFinanceira>
    {
        public InstituicaoFinanceira DadosParaInsercao(params object[] args)
        {
            var faker = new Faker<InstituicaoFinanceira>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Company.CompanyName());

            var retorno = faker.Generate();
            return retorno;
        }

        public InstituicaoFinanceira DadosParaAlteracao(InstituicaoFinanceira item)
        {
            item.Nome += " alterado";

            return item;
        }
    }
}
