using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface IAgendamentoRepositorio 
        : ICrud<Agendamento>
    {
        Task<Tuple<int, IList<AgendamentoItem>>> ContasAVencer(ICriterio criterio);
        Task<AgendamentoItem> ObterParcelaPorId(int id);
        Task<AgendamentoItem> BaixarParcela(AgendamentoItem parcela);
        Task<bool> ConciliarParcela(int id);
    }
}
