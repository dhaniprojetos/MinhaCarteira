using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class RelatorioServico
    {
        private readonly IRelatorioRepositorio _repositorio;

        public RelatorioServico(
            RelatorioRepositorio repositorio, 
            MinhaCarteiraContext ctx)
        {
            _repositorio = repositorio;
        }

        public async Task<ExtratoRelatorio> ObterRelatorioSaldos()
        {
            return await _repositorio.ObterRelatorioSaldos();
        }
    }
}
