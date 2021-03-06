using System;
using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class CentroClassificacaoBuilder : IBuilder<CentroClassificacao>
    {
        public Faker<CentroClassificacao> DadosParaInsercao(params object[] args)
        {
            Randomizer.Seed = new Random();

            var faker = new Faker<CentroClassificacao>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Person.FullName)
                .RuleFor(p => p.IdAuxiliar, f => f.Random.Int(0, 500).OrNull(f, .8f))
                .RuleFor(p => p.EhDespesa, f => f.Random.Bool())
                .RuleFor(p => p.EhReceita, f => f.Random.Bool());

            return faker;
        }

        public CentroClassificacao DadosParaAlteracao(CentroClassificacao item)
        {
            item.Nome += " alterado";

            return item;
        }
    }
}
