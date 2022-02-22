using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class AgendamentoViewModel : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public TipoMovimento Tipo { get; set; }
        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime DataInicial { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public decimal ValorReal =>
            Tipo == TipoMovimento.Credito
                ? Valor
                : Valor * (-1);

        public TipoRecorrencia TipoRecorrencia { get; set; }
        public TipoParcelas TipoParcelas { get; set; }
        public int IntervaloParcelas { get; set; }
        public DateTime? DataFinal { get; set; }
        public int Parcelas { get; set; }

        public IList<AgendamentoItemViewModel> Items { get; set; }

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        public string CaminhoCategoria => Categoria != null
            ? Categoria.Caminho
            : string.Empty;

        [DisplayName("Centro de classificação")]
        public int CentroClassificacaoId { get; set; }
        public CentroClassificacao CentroClassificacao { get; set; }
        public IEnumerable<SelectListItem> CentrosClassificacao { get; set; }
        [DisplayName("Classificação")]
        public string NomeCentroClassificacao => CentroClassificacao != null
            ? CentroClassificacao.Nome
            : string.Empty;

        [DisplayName("Pessoa")]
        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public IEnumerable<SelectListItem> Pessoas { get; set; }
        public string NomePessoa => Pessoa != null
            ? Pessoa.Nome
            : string.Empty;

        [DisplayName("Conta")]
        public int ContaBancariaId { get; set; }
        [DisplayName("Conta")]
        public ContaBancaria ContaBancaria { get; set; }
        public IEnumerable<SelectListItem> ContasBancarias { get; set; }
        public string NomeContaBancaria => ContaBancaria != null
            ? ContaBancaria.Nome
            : string.Empty;

        public AgendamentoViewModel()
        {
            var now = DateTime.Now;
            Items = new List<AgendamentoItemViewModel>();
            DataInicial = new DateTime(now.Year, now.Month, now.Day);
            DataFinal = new DateTime(now.Year, now.Month, now.Day + 5);
            Tipo = TipoMovimento.Debito;
            TipoRecorrencia = TipoRecorrencia.Mensal;
            Parcelas = 1;
            IntervaloParcelas = 1;
        }

        public void AdicionarParcela(DateTime data)
        {
            var parcela = new AgendamentoItemViewModel()
            {
                AgendamentoId = Id,
                Data = data,
                Valor = Valor,
                PessoaId = PessoaId,
                ContaBancariaId = ContaBancariaId
            };

            Items.Add(parcela);
        }
    }
}
