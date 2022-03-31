using System.Collections.Generic;

namespace MinhaCarteira.Comum.Definicao.Entidade.Relatorio
{
    public class ExtratoRelatorio
    {
        public ExtratoRelatorio()
        {
            SaldosDiario = new List<ExtratoDiario>();
            SaldosMensal = new List<ExtratoMensal>();
        }

        public ExtratoRelatorio(
            IList<ExtratoDiario> saldosDiario,
            IList<ExtratoMensal> saldosMensal)
        {
            SaldosDiario = saldosDiario;
            SaldosMensal = saldosMensal;
        }

        public IList<ExtratoDiario> SaldosDiario { get; set; }
        public IList<ExtratoMensal> SaldosMensal { get; set; }
    }
}
