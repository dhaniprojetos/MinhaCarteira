using Bogus;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Teste.Mock.Interface;
using System;

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

        public Faker<Agendamento> DadosParaInsercao(params object[] args)
        {
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
                .RuleFor(p => p.Categoria, _categoriaBuilder.DadosParaInsercao(0))
                .RuleFor(p => p.CentroClassificacao, _centroClassificacao.DadosParaInsercao())
                .RuleFor(p => p.Pessoa, f => _pessoaBuilder.DadosParaInsercao())
                .RuleFor(p => p.ContaBancaria, _contaBancaria.DadosParaInsercao())
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
