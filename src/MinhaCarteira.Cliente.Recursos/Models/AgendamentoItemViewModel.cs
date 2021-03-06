using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MinhaCarteira.Cliente.Recursos.Models.Enum;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Cliente.Recursos.Models.Base;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class AgendamentoItemViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int AgendamentoId { get; set; }
        public AgendamentoViewModel Agendamento { get; set; }
        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime Data { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Valor { get; set; }
        public bool EstahPaga { get; set; }
        public bool EstahConciliada { get; set; }

        private DateTime? dataPagamento;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? DataPagamento
        {
            get => dataPagamento ?? Data;
            set => dataPagamento = value;
        }
        private decimal? valorPago;
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? ValorPago
        {
            get => valorPago ?? Valor;
            set => valorPago = value;
        }

        [Range(1, int.MaxValue)]
        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public string NomePessoa => Pessoa != null
            ? Pessoa.Nome
            : Agendamento?.NomePessoa ?? string.Empty;

        [Range(1, int.MaxValue)]
        public int? ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public string NomeContaBancaria => ContaBancaria != null
            ? ContaBancaria.Nome
            : string.Empty;

        public StatusParcela StatusParcela
        {
            get
            {
                var mesAnoConta = int.Parse($"{Data.Year}{Data.Month:d2}");
                var mesAnoAtual = int.Parse($"{DateTime.Now.Year}{DateTime.Now.Month:d2}");

                return EstahPaga switch
                {
                    true when EstahConciliada => StatusParcela.Conciliada,
                    true when !EstahConciliada => StatusParcela.Paga,
                    false when mesAnoAtual > mesAnoConta => StatusParcela.Vencida,
                    _ => StatusParcela.Aberta
                };
            }
        }

        public AgendamentoItemViewModel()
        {
            Data = DateTime.Now;
            Agendamento = new AgendamentoViewModel();
        }
    }
}
