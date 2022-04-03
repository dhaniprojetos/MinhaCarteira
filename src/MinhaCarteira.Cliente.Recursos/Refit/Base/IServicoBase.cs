using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit.Base
{
    [Headers("Authorization: Bearer")]
    public interface IServicoBase<TEntidade>
        where TEntidade : IEntidade
    {
        [Get("")]
        Task<RespostaPaginada<IList<TEntidade>>> Navegar([Body] ICriterio criterio);

        [Get("/{id}")]
        Task<Resposta<TEntidade>> ObterPorId(int id);

        [Post("")]
        Task<Resposta<TEntidade>> Incluir(TEntidade item);

        [Put("")]
        Task<Resposta<TEntidade>> Alterar(TEntidade item);

        [Delete("")]
        Task<Resposta<int>> Deletar(int id);
    }
}
