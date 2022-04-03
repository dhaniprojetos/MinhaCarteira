using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IAgendamentoServico : IServicoCrud<Agendamento, ICrud<Agendamento>>
    {
        Task<Tuple<int, IList<AgendamentoItem>>> ContasAVencer(ICriterio filtro);
        Task<AgendamentoItem> ObterParcelaPorId(int id);
        Task<AgendamentoItem> BaixarParcela(AgendamentoItem id);
        Task<bool> ConciliarParcela(int id, string idMovimentos);
    }
}
