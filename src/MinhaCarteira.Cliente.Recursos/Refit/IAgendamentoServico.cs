﻿using MinhaCarteira.Cliente.Recursos.Refit.Base;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    [Headers("Authorization: Bearer")]
    public interface IAgendamentoServico : IServicoBase<Agendamento>
    {
        [Get("/contas-a-vencer/{qtdDias}")]
        Task<Resposta<IList<AgendamentoItem>>> ContasAVencer(int qtdDias);
    }
}