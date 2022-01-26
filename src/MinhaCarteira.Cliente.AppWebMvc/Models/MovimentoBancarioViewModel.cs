using System;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class MovimentoBancarioViewModel : IEntidade
    {
        public int Id { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public DateTime DataMovimento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int? IdAuxiliar { get; set; }

        public CentroClassificacao CentroClassificacao { get; set; }
        public Pessoa Pessoa { get; set; }
        public Categoria Categoria { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        public decimal ValorReal
        {
            get => TipoMovimento == TipoMovimento.Credito
                ? Valor
                : Valor * (-1);
        }
    }
}
