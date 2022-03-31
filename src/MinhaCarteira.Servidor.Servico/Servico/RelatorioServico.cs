using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class RelatorioServico
    {
        private readonly RelatorioRepositorio _repositorio;
        private readonly MinhaCarteiraContext _ctx;

        public RelatorioServico(
            RelatorioRepositorio repositorio, 
            MinhaCarteiraContext ctx)
        {
            _repositorio = repositorio;
            _ctx = ctx;
        }

        public async Task<ExtratoRelatorio> ObterRelatorioSaldos()
        {
            return await _repositorio.ObterRelatorioSaldos(_ctx);
        }
    }
}
