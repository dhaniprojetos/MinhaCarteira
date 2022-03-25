using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinhaCarteira.Cliente.Recursos.Models.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class ContaBancariaViewModel : BaseViewModel, IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm:ss", ApplyFormatInEditMode = true)]
        public DateTime DataSaldoInicial { get; set; }
        [DataType(DataType.Currency)]
        public decimal ValorSaldoInicial { get; set; }
        [DataType(DataType.Currency)]
        public decimal ValorSaldoAtual { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Instituição Financeira")]
        public int InstituicaoFinanceiraId { get; set; }
        public InstituicaoFinanceiraViewModel InstituicaoFinanceira { get; set; }
        public string InstituicaoFinanceiraNome => InstituicaoFinanceira?.Nome;

        public IEnumerable<SelectListItem> InstituicoesBancaria { get; set; }

        public ContaBancariaViewModel()
        {
            InstituicoesBancaria = new List<SelectListItem>();
            DataSaldoInicial = DateTime.Today;
        }
        public ContaBancariaViewModel(Resposta<IList<InstituicaoFinanceira>> resposta)
        {
            AdicionarInstituicoesFinanceiras(resposta.Dados);
        }

        public void AdicionarInstituicoesFinanceiras(IList<InstituicaoFinanceira> items)
        {
            var instituicoes = items?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToList();

            InstituicoesBancaria = instituicoes;
        }
    }
}
