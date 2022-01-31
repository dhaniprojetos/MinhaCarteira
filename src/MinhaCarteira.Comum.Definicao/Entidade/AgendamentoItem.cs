using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System;
using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class AgendamentoItem : IEntidade
    {
        public int Id { get; set; }
        public int AgendamentoId { get; set; }
        public Agendamento Agendamento { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public bool EstahPaga { get; set; }

        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public int? ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }

        public IList<MovimentoBancario> Movimentos { get; set; }
    }
}
