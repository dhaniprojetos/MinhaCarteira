using MinhaCarteira.Comum.Definicao.Entidade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IAgendamentoServico : IServicoCrud<Agendamento>
    {
        Task<IList<AgendamentoItem>> ContasAVencer(int qtdDias);
    }
}
