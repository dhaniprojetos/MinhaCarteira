using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface ICrud<TEntidade> : IDisposable
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
