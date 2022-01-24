using System;

namespace MinhaCarteira.Comum.Definicao.Modelo.Servico
{
    public class RespostaPaginada<T> : Resposta<T>
    {
        public int NumeroPagina { get; set; }
        public int ItensPorPagina { get; set; }
        public Uri PrimeiraPagina { get; set; }
        public Uri UltimaPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public Uri ProximaPagina { get; set; }
        public Uri PaginaAnterior { get; set; }

        public RespostaPaginada(
            T dados,
            int numeroPagina,
            int itensPorPagina)
        {
            NumeroPagina = numeroPagina;
            ItensPorPagina = itensPorPagina;
            Dados = dados;
            Mensagem = null;
            BemSucedido = true;
            Erros = null;
        }
    }
}
