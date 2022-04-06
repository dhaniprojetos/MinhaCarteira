using System.Threading.Tasks;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    public interface IUsuarioServico : IServicoBase<Usuario>
    {
        [Post("/login")]
        Task<Resposta<UsuarioToken>> Logar(UsuarioLogin item);

        [Post("/atualizar-condicao-sidebar")]
        Task<Resposta<bool>> AtualizarCondicaoSidebar(string username, string condicao);
    }
}
