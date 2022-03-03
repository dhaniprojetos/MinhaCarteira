using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit.Base
{
    [Headers("Authorization: Bearer")]
    public interface IServicoBase<TEntidade, TCriterio>
        where TEntidade : IEntidade
        where TCriterio : ICriterio<TEntidade>
    {
        [Get("")]
        Task<Resposta<IList<TEntidade>>> Navegar(TCriterio criterio);

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
