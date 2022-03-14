using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class ConciliarAgendamentoViewModel
    {
        public ConciliarAgendamentoViewModel()
        {

        }

        public ConciliarAgendamentoViewModel(
            IList<ContaBancariaViewModel> contas,
            AgendamentoItemViewModel parcela)
        {
            ContasBancarias = contas
                .Select(s => new SelectListItem(s.Nome, s.Id.ToString()))
                .ToList();

            ContaBancariaId = parcela.ContaBancariaId ?? parcela.Agendamento.ContaBancariaId;

            Parcela = parcela;

            DataInicial = (parcela.DataPagamento ?? parcela.Data).AddDays(-3);
            DataFinal = (parcela.DataPagamento ?? parcela.Data).AddDays(3);

            ValorInicial = (parcela.ValorPago ?? parcela.Valor) - 10;
            ValorFinal = (parcela.ValorPago ?? parcela.Valor) + 10;
        }

        public IEnumerable<SelectListItem> ContasBancarias { get; set; }
        public AgendamentoItemViewModel Parcela { get; set; }

        public int ContaBancariaId { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal ValorInicial { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal ValorFinal { get; set; }
        public string Descricao { get; set; }
    }
}
