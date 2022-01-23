using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class PessoaBuilder : IBuilder<Pessoa>
    {
        public Pessoa DadosParaAlteracao(Pessoa item)
        {
            item.Nome += " alterado";

            return item;
        }

        public Pessoa DadosParaInsercao(params object[] args)
        {
            var faker = new Faker<Pessoa>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Person.FullName);

            var retorno = faker.Generate();
            return retorno;
        }
    }
}
