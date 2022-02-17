using System;
using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class MovimentoBancarioBuilder : IBuilder<MovimentoBancario>
    {
        private readonly IBuilder<Pessoa> _pessoaBuilder;
        private readonly IBuilder<Categoria> _categoriaBuilder;
        private readonly IBuilder<ContaBancaria> _contaBancaria;
        private readonly IBuilder<CentroClassificacao> _centroClassificacao;

        public MovimentoBancarioBuilder(
            IBuilder<Pessoa> pessoaBuilder, 
            IBuilder<Categoria> categoriaBuilder, 
            IBuilder<ContaBancaria> contaBancaria, 
            IBuilder<CentroClassificacao> centroClassificacao)
        {
            _pessoaBuilder = pessoaBuilder;
            _categoriaBuilder = categoriaBuilder;
            _contaBancaria = contaBancaria;
            _centroClassificacao = centroClassificacao;
        }

        public Faker<MovimentoBancario> DadosParaInsercao(params object[] args)
        {
            var now = DateTime.Now;
            var ultimoDia = DateTime.DaysInMonth(now.Year, now.Month);
            var dataInicial = new DateTime(now.Year, now.Month, 1);
            var dataFinal = new DateTime(now.Year, now.Month, ultimoDia);

            var faker = new Faker<MovimentoBancario>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.TipoMovimento, f => f.PickRandom<TipoMovimento>())
                .RuleFor(p => p.DataMovimento, f => f.Date.Between(dataInicial, dataFinal))
                .RuleFor(p => p.Descricao, f => f.Random.Words(5))
                .RuleFor(p => p.Valor, f=> f.Random.Decimal())
                .RuleFor(p => p.IdAuxiliar, f => f.Random.Int(0, 500).OrNull(f, .8f))
                .RuleFor(p => p.CentroClassificacao, _centroClassificacao.DadosParaInsercao())
                .RuleFor(p => p.Pessoa, _pessoaBuilder.DadosParaInsercao())
                .RuleFor(p => p.Categoria, _categoriaBuilder.DadosParaInsercao(0))
                .RuleFor(p => p.ContaBancaria, _contaBancaria.DadosParaInsercao());

            return faker;
        }

        public MovimentoBancario DadosParaAlteracao(MovimentoBancario item)
        {
            item.Descricao += " alterado";

            return item;
        }
    }
}
