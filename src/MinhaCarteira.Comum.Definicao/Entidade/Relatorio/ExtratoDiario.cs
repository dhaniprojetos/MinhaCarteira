using System;

namespace MinhaCarteira.Comum.Definicao.Entidade.Relatorio
{
    public class ExtratoDiario
    {
        public int Idx { get; set; }
        public DateTime Data { get; set; }
        public int ContaBancariaId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
