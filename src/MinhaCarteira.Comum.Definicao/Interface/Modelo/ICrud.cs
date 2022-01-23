using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface ICrud<TEntidade> : IDisposable
    {
        Task<int> Deletar(int[] ids);
        Task<IList<TEntidade>> Alterar(IList<TEntidade> itens);
        Task<IList<TEntidade>> Navegar(ICriterio<TEntidade> criterio);
        Task<IList<TEntidade>> Incluir(IList<TEntidade> itens);

        Task<TEntidade> ObterPorId(int id);
    }
}
