using System;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class ContaBancaria : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public DateTime DataSaldoInicial { get; set; }
        public decimal ValorSaldoInicial { get; set; }
        public decimal ValorSaldoAtual { get; set; }

        public int InstituicaoFinanceiraId { get; set; }
        public InstituicaoFinanceira InstituicaoFinanceira { get; set; }
    }
}
