﻿using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
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

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        public string CaminhoCategoria => Categoria != null
            ? Categoria.Caminho
            : string.Empty;

        [DisplayName("Centro de classificação")]
        public int CentroClassificacaoId { get; set; }
        public CentroClassificacao CentroClassificacao { get; set; }
        public IEnumerable<SelectListItem> CentrosClassificacao { get; set; }
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
            DataInicial = new DateTime(now.Year, now.Month, now.Day);
            Tipo = TipoMovimento.Debito;
        }
    }
}
