using System;
using System.Collections.Generic;
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

        private Tuple<IList<CentroClassificacao>, IList<Pessoa>, IList<Categoria>, IList<ContaBancaria>> ObterDados(params object[] args)
        {
            IList<CentroClassificacao> centros = args != null
                && args.Length > 0
                && args[0] is IList<CentroClassificacao> retornoCentros
                ? retornoCentros
                : new List<CentroClassificacao>(_centroClassificacao.DadosParaInsercao().Generate(1));

            IList<Pessoa> pessoas = args != null
                && args.Length > 1
                && args[1] is IList<Pessoa> retornoPessoas
                ? retornoPessoas
                : new List<Pessoa>(_pessoaBuilder.DadosParaInsercao().Generate(1));

            IList<Categoria> categorias = args != null
                && args.Length > 2
                && args[2] is IList<Categoria> retornoCategorias
                ? retornoCategorias
                : new List<Categoria>(_categoriaBuilder.DadosParaInsercao().Generate(1));

            IList<ContaBancaria> contas = args != null
                && args.Length > 3
                && args[3] is IList<ContaBancaria> retornoContas
                ? retornoContas
                : new List<ContaBancaria>(_contaBancaria.DadosParaInsercao().Generate(1));

            return new Tuple<IList<CentroClassificacao>, IList<Pessoa>, IList<Categoria>, IList<ContaBancaria>>(
                centros, pessoas, categorias, contas);
        }

        public Faker<MovimentoBancario> DadosParaInsercao(params object[] args)
        {
            var dados = ObterDados(args);
            var now = DateTime.Now;
            var ultimoDia = DateTime.DaysInMonth(now.Year, now.Month);
            var dataInicial = new DateTime(now.Year, now.Month, 1);
            var dataFinal = new DateTime(now.Year, now.Month, ultimoDia);

            var faker = new Faker<MovimentoBancario>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.TipoMovimento, f => f.PickRandom<TipoMovimento>())
                .RuleFor(p => p.DataMovimento, f => f.Date.Between(dataInicial, dataFinal))
                .RuleFor(p => p.Descricao, f => f.Random.Words(5))
                .RuleFor(p => p.Valor, f => f.Random.Decimal(0, 999))
                .RuleFor(p => p.IdAuxiliar, f => f.Random.Int(0, 500).OrNull(f, .8f))
                .RuleFor(p => p.CentroClassificacao, f => f.PickRandom(dados.Item1))
                .RuleFor(p => p.Pessoa, f => f.PickRandom(dados.Item2))
                .RuleFor(p => p.Categoria, f => f.PickRandom(dados.Item3))
                .RuleFor(p => p.ContaBancaria, f => f.PickRandom(dados.Item4));

            return faker;
        }

        public MovimentoBancario DadosParaAlteracao(MovimentoBancario item)
        {
            item.Descricao += " alterado";

            return item;
        }
    }
}
