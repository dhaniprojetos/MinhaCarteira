using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Teste.Mock.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class AgendamentoBuilder : IBuilder<Agendamento>
    {
        private readonly IBuilder<Pessoa> _pessoaBuilder;
        private readonly IBuilder<Categoria> _categoriaBuilder;
        private readonly IBuilder<ContaBancaria> _contaBancaria;
        private readonly IBuilder<CentroClassificacao> _centroClassificacao;

        public AgendamentoBuilder(
            IBuilder<Pessoa> pessoaBuilder,
            IBuilder<Categoria> categoriaBuilder,
            IBuilder<ContaBancaria> contaBancaria,
            IBuilder<CentroClassificacao> centroClassificacao)
        {
            _contaBancaria = contaBancaria;
            _pessoaBuilder = pessoaBuilder;
            _categoriaBuilder = categoriaBuilder;
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

        public Faker<Agendamento> DadosParaInsercao(params object[] args)
        {
            var dados = ObterDados(args);
            var now = DateTime.Now;
            var ultimoDia = DateTime.DaysInMonth(now.Year, now.Month);
            var dataInicial = new DateTime(now.Year, now.Month, 1);
            var dataFinal = new DateTime(now.Year, now.Month, ultimoDia);

            var pessoa = _pessoaBuilder.DadosParaInsercao();

            var faker = new Faker<Agendamento>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Tipo, f => f.PickRandom<TipoMovimento>())
                .RuleFor(p => p.DataInicial, f => f.Date.Between(dataInicial, dataFinal))
                .RuleFor(p => p.Descricao, f => f.Random.Words(5))
                .RuleFor(p => p.Valor, f => f.Random.Decimal(0, 999))
                .RuleFor(p => p.IdAuxiliar, f => f.Random.Int(0, 500).OrNull(f, .8f))
                .RuleFor(p => p.CentroClassificacao, f => f.PickRandom(dados.Item1))
                .RuleFor(p => p.Pessoa, f => f.PickRandom(dados.Item2))
                .RuleFor(p => p.Categoria, f => f.PickRandom(dados.Item3))
                .RuleFor(p => p.ContaBancaria, f => f.PickRandom(dados.Item4))
                .RuleFor(p => p.Tipo, f => f.PickRandom<TipoMovimento>())
                .RuleFor(p => p.TipoParcelas, f => (TipoParcelas)f.Random.Int(1, 2))
                .RuleFor(p => p.Parcelas, f => f.Random.Int(1, 20))
                .RuleFor(p => p.TipoRecorrencia, f => f.PickRandom<TipoRecorrencia>());

            return faker;
        }

        public Agendamento DadosParaAlteracao(Agendamento item)
        {
            item.Descricao += " alterado";
            return item;
        }
    }
}
