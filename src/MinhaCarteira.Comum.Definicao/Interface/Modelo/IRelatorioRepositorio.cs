using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using System;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface IRelatorioRepositorio
    {
        Task<ExtratoRelatorio> ObterRelatorioSaldos(DateTime inicio, DateTime fim);
    }
}
