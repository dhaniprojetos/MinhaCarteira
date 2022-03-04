using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface ICrud<TEntidade> : IDisposable
    {
        Task<int> Deletar(int id);
        Task<TEntidade> Alterar(TEntidade item);
        Task<IList<TEntidade>> Navegar(ICriterio<TEntidade> criterio);
        Task<TEntidade> Incluir(TEntidade item);
        
        Task<int> DeletarRange(int[] ids);
        Task<IList<TEntidade>> AlterarRange(IList<TEntidade> itens);
        Task<IList<TEntidade>> IncluirRange(IList<TEntidade> itens);

        Task<TEntidade> ObterPorId(int id);
    }
}
