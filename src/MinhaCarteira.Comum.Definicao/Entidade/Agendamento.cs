using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using System;
using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Agendamento : IEntidade
    {
        public Agendamento()
        {
            Items = new List<AgendamentoItem>();
        }

        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        public TipoMovimento Tipo { get; set; }
        public DateTime DataInicial { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string RegraRecorrencia { get; set; }
        public int Parcelas { get; set; }
        public TipoParcelas TipoParcelas { get; set; }
        public TipoRecorrencia TipoRecorrencia { get; set; }

        public IList<AgendamentoItem> Items { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int CentroClassificacaoId { get; set; }
        public CentroClassificacao CentroClassificacao { get; set; }

        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public int? ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        public void AdicionarParcela(DateTime data)
        {
            var parcela = new AgendamentoItem()
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
