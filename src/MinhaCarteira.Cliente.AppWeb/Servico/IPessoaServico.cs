using MinhaCarteira.Cliente.AppWeb.ViewModel;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using Refit;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWeb.Servico
{
    public interface IPessoaServico
    {
        [Post("pessoas")]
        Task<PessoaViewModel> Navegar(ICriterio<Pessoa> criterio);
    }
}
