using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo.Base
{
    public interface ICrud<TEntidade> : IDisposable
        where TEntidade: class, IEntidade
    {
        //int TotalRegistros { get; set; }
        Task<int> Deletar(int id);
        Task<TEntidade> Alterar(TEntidade item);
        Task<Tuple<int, IList<TEntidade>>> Navegar(ICriterio criterio);
        Task<TEntidade> Incluir(TEntidade item);

        Task<int> DeletarRange(int[] ids);
        Task<IList<TEntidade>> AlterarRange(IList<TEntidade> itens);
        Task<IList<TEntidade>> IncluirRange(IList<TEntidade> itens);

        Task<TEntidade> ObterPorId(int id);
    }
}
