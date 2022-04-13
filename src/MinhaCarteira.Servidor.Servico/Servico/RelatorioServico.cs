using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using System;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class RelatorioServico
    {
        private readonly IRelatorioRepositorio _repositorio;

        public RelatorioServico(
            RelatorioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ExtratoRelatorio> ObterRelatorioSaldos(DateTime inicio, DateTime fim)
        {
            return await _repositorio.ObterRelatorioSaldos(inicio, fim);
        }
    }
}
