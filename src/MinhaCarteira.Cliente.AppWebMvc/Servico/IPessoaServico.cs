using MinhaCarteira.Cliente.AppWebMvc.ViewModel;
using MinhaCarteira.Comum.Definicao.Entidade;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Servico
{
    public interface IPessoaServico
    {
        [Get("/pessoa")]
        Task<IList<PessoaViewModel>> Navegar();

        [Get("/pessoa/{id}")]
        Task<PessoaViewModel> ObterPorId(int id);

        [Post("/pessoa")]
        Task<IList<PessoaViewModel>> Incluir(IList<Pessoa> itens);

        [Put("/pessoa")]
        Task<IList<PessoaViewModel>> Alterar(IList<Pessoa> itens);

        [Delete("/pessoa")]
        Task<int> Deletar(int id);
    }
}
