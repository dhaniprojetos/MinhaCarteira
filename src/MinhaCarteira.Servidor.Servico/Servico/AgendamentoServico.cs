using Dates.Recurring;
using Dates.Recurring.Type;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class AgendamentoServico : ServicoBase<Agendamento>, IAgendamentoServico
    {
        private RecurrenceType ObterRecorrenciaBuilder(Agendamento agend)
        {
            return agend.TipoRecorrencia switch
            {
                TipoRecorrencia.Semanal => Recurs
                    .Starting(agend.DataInicial)
                    .Every(1)
                    .Weeks()
                    .FirstDayOfWeek(System.DayOfWeek.Monday)
                    .OnDay(agend.DataInicial.DayOfWeek)
                    .Ending(agend.DataInicial.AddYears(20))
                    .Build(),

                TipoRecorrencia.Mensal => Recurs
                    .Starting(agend.DataInicial)
                    .Every(1)
                    .Months()
                    .OnDay(agend.DataInicial.Day)
                    .Ending(agend.DataInicial.AddYears(20))
                    .Build(),

                TipoRecorrencia.Anual => Recurs
                    .Starting(agend.DataInicial)
                    .Every(1)
                    .Years()
                    .OnDay(agend.DataInicial.Day)
                    .OnMonths((Month)agend.DataInicial.Month)
                    .Ending(agend.DataInicial.AddYears(20))
                    .Build(),

                _ => Recurs
                    .Starting(agend.DataInicial)
                    .Every(1)
                    .Days()
                    .Ending(agend.DataInicial.AddYears(20))
                    .Build()
            };
        }

        private Agendamento GerarParcelas(Agendamento agend)
        {
            var recorrencia = ObterRecorrenciaBuilder(agend);
            DateTime data = agend.DataInicial;
            DateTime? proximo;

            do
            {
                agend.AdicionarParcela(data);

                proximo = recorrencia.Next(data);
                if (proximo.HasValue)
                    data = new DateTime(
                        proximo.Value.Year,
                        proximo.Value.Month,
                        proximo.Value.Day);

                switch (agend.TipoParcelas)
                {
                    case TipoParcelas.Parcelada:
                        if (agend.Items.Count >= agend.Parcelas)
                            proximo = null;

                        break;
                    case TipoParcelas.Recorrente:
                        if (proximo >= agend.DataInicial.AddYears(5))
                            proximo = null;

                        break;
                    case TipoParcelas.Unica:
                    default:
                        proximo = null;

                        break;
                }
            } while (proximo != null);

            return agend;
        }

        public AgendamentoServico(ICrud<Agendamento> repositorio)
            : base(repositorio) { }

        public override async Task<Agendamento> Incluir(Agendamento item)
        {
            item = GerarParcelas(item);
            var itemDb = await base.Incluir(item);
            item.Items.Clear();
            return itemDb;
        }

        public async Task<IList<AgendamentoItem>> ContasAVencer(int qtdDias)
        {
            var itens = await ((AgendamentoRepositorio)Repositorio)
                .ContasAVencer(qtdDias);

            return itens;
        }

    }
}
