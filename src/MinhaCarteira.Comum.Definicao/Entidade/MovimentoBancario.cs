using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using System;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class MovimentoBancario : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public DateTime DataMovimento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorReal =>
            TipoMovimento == TipoMovimento.Credito
                ? Valor
                : Valor * (-1);

        public int CentroClassificacaoId { get; set; }
        public CentroClassificacao CentroClassificacao { get; set; }

        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        public int? AgendamentoItemId { get; set; }
        public AgendamentoItem AgendamentoItem { get; set; }
    }
}