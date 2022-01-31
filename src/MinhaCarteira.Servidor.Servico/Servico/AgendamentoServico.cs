using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class AgendamentoServico : ServicoBase<Agendamento>
    {
        public AgendamentoServico(ICrud<Agendamento> repositorio) : base(repositorio)
        {
        }
    }
}
