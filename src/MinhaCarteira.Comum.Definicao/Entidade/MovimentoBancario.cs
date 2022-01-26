using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class MovimentoBancario : IEntidade
    {
        public MovimentoBancario()
        {
            DataMovimento = DateTime.Now;
        }

        public int Id { get; set; }
        public TipoMovimento TipoMovimento { get; set; }   
        public DateTime DataMovimento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int? IdAuxiliar { get; set; }

        public int CentroClassificacaoId { get; set; }
        public CentroClassificacao CentroClassificacao { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        [NotMapped]
        public decimal ValorReal
        {
            get => TipoMovimento == TipoMovimento.Credito
                ? Valor
                : Valor * (-1);
        }
    }
}

/*

        [ForeignKey("Id_CentroClassificacao")]
        public virtual CentroClassificacao CentroClassificacao
        {
            get => centroClassificacao;
            set => SetProperty(ref centroClassificacao, value);
        }
        [ForeignKey("Id_Pessoa")]
        public virtual Pessoa Pessoa
        {
            get => pessoa;
            set => SetProperty(ref pessoa, value);
        }
        [ForeignKey("Id_Categoria")]
        public virtual Categoria Categoria
        {
            get => categoria;
            set => SetProperty(ref categoria, value);
        }
        [ForeignKey("Id_Conta_Bancaria")]
        public virtual ContaBancaria ContaBancaria
        {
            get => contaBancaria;
            set => SetProperty(ref contaBancaria, value);
        }
    }
}
*/