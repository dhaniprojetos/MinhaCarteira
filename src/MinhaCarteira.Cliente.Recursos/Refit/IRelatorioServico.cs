using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;
namespace MinhaCarteira.Cliente.Recursos.Refit
{
    [Headers("Authorization: Bearer")]
    public interface IRelatorioServico
    {
        [Get("/obter-extratos")]
        Task<Resposta<ExtratoRelatorio>> ObterExtratos();
    }
}
