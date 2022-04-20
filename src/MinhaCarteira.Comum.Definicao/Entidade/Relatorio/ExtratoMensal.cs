namespace MinhaCarteira.Comum.Definicao.Entidade.Relatorio
{
    public class ExtratoMensal
    {
        public int Idx { get; set; }
        public string ContaBancariaNome { get; set; }
        public string MesAno { get; set; }
        //public string Descricao { get; set; }
        //public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
