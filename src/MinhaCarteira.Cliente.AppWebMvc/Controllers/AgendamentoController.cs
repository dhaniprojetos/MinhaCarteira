using AutoMapper;
using Dates.Recurring;
using Dates.Recurring.Type;
using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Cliente.AppWebMvc.Controllers.Base;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using System;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class AgendamentoController : BaseController<Agendamento, AgendamentoViewModel>
    {
        public AgendamentoController(
            IServicoBase<Agendamento> servico,
            IMapper mapper)
            : base(servico, mapper) { }

        private RecurrenceType ObterRecorrenciaBuilder(AgendamentoViewModel viewModel)
        {
            switch (viewModel.TipoRecorrencia)
            {
                case Comum.Definicao.Modelo.TipoRecorrencia.Semanal:
                    return Recurs
                        .Starting(viewModel.DataInicial)
                        .Every(viewModel.IntervaloParcelas)
                        .Weeks()
                        .Build();
                case Comum.Definicao.Modelo.TipoRecorrencia.Mensal:
                    return Recurs
                        .Starting(viewModel.DataInicial)
                        .Every(viewModel.IntervaloParcelas)
                        .Months()
                        .Build();
                case Comum.Definicao.Modelo.TipoRecorrencia.Anual:
                    return Recurs
                        .Starting(viewModel.DataInicial)
                        .Every(viewModel.IntervaloParcelas)
                        .Years()
                        .Build();
                default:
                    return Recurs
                        .Starting(viewModel.DataInicial)
                        .Every(viewModel.IntervaloParcelas)
                        .Days()
                        .Build();
            }
        }

        protected override async Task<AgendamentoViewModel> InicializarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(viewModel);
        }
        protected override async Task<bool> ValidarViewModel(AgendamentoViewModel viewModel)
        {
            return await Task.FromResult(true);
        }
        protected override async Task<Tuple<AgendamentoViewModel, Agendamento>> ExecutarAntesSalvar(AgendamentoViewModel viewModel, Agendamento model)
        {
            var agendamento = ObterRecorrenciaBuilder(viewModel);
            DateTime data = viewModel.DataInicial;
            DateTime? proximo;

            do
            {
                viewModel.AdicionarParcela(data);

                proximo = agendamento.Next(data);
                if (proximo.HasValue)
                    data = new DateTime(
                        proximo.Value.Year,
                        proximo.Value.Month,
                        proximo.Value.Day);

                switch (viewModel.TipoParcelas)
                {
                    case TipoParcelas.Parcelada:
                        if (viewModel.Items.Count >= viewModel.Parcelas)
                            proximo = null;

                        break;
                    case TipoParcelas.Recorrente:
                        if (viewModel.DataFinal != null)
                        {
                            if (proximo >= viewModel.DataFinal)
                                proximo = null;
                        }
                        else
                        {
                            if (proximo >= viewModel.DataInicial.AddYears(5))
                                proximo = null;
                        }

                        break;
                    case TipoParcelas.Unica:
                    default:
                        proximo = null;

                        break;
                }
            } while (proximo != null);

            model = Mapper.Map<Agendamento>(viewModel);

            return await base.ExecutarAntesSalvar(viewModel, model);
        }

        #region Métodos sobrescritos apenas manter as views
        public override async Task<IActionResult> Index()
        {
            return await base.Index();
        }

        public override async Task<IActionResult> Criar()
        {
            return await base.Criar();
        }

        public override async Task<IActionResult> Detalhes(int id)
        {
            return await base.Detalhes(id);
        }

        public override async Task<IActionResult> Alterar(int id)
        {
            return await base.Alterar(id);
        }

        public override async Task<IActionResult> Deletar(int id)
        {
            return await base.Deletar(id);
        }
        #endregion
    }
}
