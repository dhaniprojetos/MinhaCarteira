using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using System;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class PessoaBuilder : IBuilder<Pessoa>
    {
        public Faker<Pessoa> DadosParaInsercao(params object[] args)
        {
            Randomizer.Seed = new Random();

            var faker = new Faker<Pessoa>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => f.Person.FullName)
                .RuleFor(p => p.IdAuxiliar, f => f.Random.Int(0, 500).OrNull(f, .8f))
                .RuleFor(p => p.EhCliente, f => f.Random.Bool())
                .RuleFor(p => p.EhFornecedor, f => f.Random.Bool());

            return faker;
        }
        
        public Pessoa DadosParaAlteracao(Pessoa item)
        {
            item.Nome += " alterado";

            return item;
        }
    }
}
