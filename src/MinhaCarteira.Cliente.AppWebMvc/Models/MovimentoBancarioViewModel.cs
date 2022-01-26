using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class MovimentoBancarioViewModel : IEntidade
    {
        public int Id { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime DataMovimento { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int? IdAuxiliar { get; set; }

        [DisplayName("Centro de classificação")]
        public int CentroClassificacaoId { get; set; }
        public CentroClassificacao CentroClassificacao { get; set; }
        public IEnumerable<SelectListItem> CentrosClassificacao { get; set; }

        [DisplayName("Pessoa")]
        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public IEnumerable<SelectListItem> Pessoas { get; set; }

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

        [DisplayName("Conta")]
        public int ContaBancariaId { get; set; }
        [DisplayName("Conta")]
        public ContaBancaria ContaBancaria { get; set; }
        public IEnumerable<SelectListItem> ContasBancarias { get; set; }

        public decimal ValorReal =>
            TipoMovimento == TipoMovimento.Credito
                ? Valor
                : Valor * (-1);

        public MovimentoBancarioViewModel()
        {
            var now = DateTime.Now;
            DataMovimento = new DateTime(now.Year, now.Month, now.Day);
            TipoMovimento = TipoMovimento.Debito;
            
            Pessoas = new List<SelectListItem>();
            CentrosClassificacao = new List<SelectListItem>();
            Categorias = new List<SelectListItem>();
            ContasBancarias = new List<SelectListItem>();
        }

        public void AdicionarPessoas(IList<Pessoa> items)
        {
            var valores = items?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToList();

            Pessoas = valores;
        }

        public void AdicionarCentrosClassificacao(IList<CentroClassificacao> items)
        {
            var valores = items?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToList();

            CentrosClassificacao = valores;
        }

        public void AdicionarCategorias(IList<Categoria> items)
        {
            var valores = items?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToList();

            Categorias= valores;
        }

        public void AdicionarContasBancarias(IList<ContaBancaria> items)
        {
            var valores = items?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToList();

            ContasBancarias = valores;
        }
    }
}
