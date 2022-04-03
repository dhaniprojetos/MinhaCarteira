using Dates.Recurring;
using Dates.Recurring.Type;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class AgendamentoServico : ServicoBase<Agendamento, ICrud<Agendamento>>, IAgendamentoServico
    {
        private readonly ICrud<MovimentoBancario> _movimentoRepositorio;

        public AgendamentoServico(
            ICrud<Agendamento> repositorio,
            ICrud<MovimentoBancario> movimentoRepositorio)
            : base(repositorio)
        {
            _movimentoRepositorio = movimentoRepositorio;
        }

        private static RecurrenceType ObterRecorrenciaBuilder(Agendamento agend)
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

        private static Agendamento GerarParcelas(Agendamento agend)
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

        public override async Task<Agendamento> Incluir(Agendamento item)
        {
            item.TipoRecorrencia = item.TipoParcelas == TipoParcelas.Parcelada
                ? TipoRecorrencia.Mensal
                : item.TipoRecorrencia;

            item = GerarParcelas(item);
            var itemDb = await base.Incluir(item);
            item.Items.Clear();
            return itemDb;
        }

        public async Task<Tuple<int, IList<AgendamentoItem>>> ContasAVencer(ICriterio filtro)
        {
            var itens = await ((AgendamentoRepositorio)Repositorio)
                .ContasAVencer(filtro);

            return itens;
        }

        public async Task<AgendamentoItem> ObterParcelaPorId(int id)
        {
            var item = await ((AgendamentoRepositorio)Repositorio)
                .ObterParcelaPorId(id);

            return item;
        }

        public async Task<AgendamentoItem> BaixarParcela(AgendamentoItem parcela)
        {
            var item = await ((AgendamentoRepositorio)Repositorio)
                .BaixarParcela(parcela);

            return item;
        }

        public async Task<bool> ConciliarParcela(int id, string idMovimentos)
        {
            var _ = await ((MovimentoBancarioRepositorio)_movimentoRepositorio)
                .ConciliarParcela(id, idMovimentos);
            
            var alterado = await ((AgendamentoRepositorio)Repositorio)
                .ConciliarParcela(id);

            return alterado;
        }
    }
}
