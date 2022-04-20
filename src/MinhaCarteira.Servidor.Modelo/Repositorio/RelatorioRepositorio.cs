using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using System;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        private readonly MinhaCarteiraContext _contexto;

        public RelatorioRepositorio(MinhaCarteiraContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<ExtratoRelatorio> ObterRelatorioSaldos(DateTime inicio, DateTime fim)
        {
            var cmdSql = @"
    drop table if exists #extrato;
    drop table if exists #extratoDiario;
    drop table if exists #extratoMensal;


    /*--------------------------------------------------+
    | MONTAGEM DO EXTRATO DOS MOVIMENTOS E AGENDAMENTOS |
    +--------------------------------------------------*/
    select CAST(row_number() over(partition by grp.ContaBancariaId, grp.Data order by grp.Data) as int) Idx
         , grp.ContaBancariaId
         , conta.Nome ContaBancariaNome
         , CONVERT(DATE, grp.Data) Data
         , grp.Descricao
         , grp.Valor
         , sum(grp.Valor) over (partition by grp.ContaBancariaId order by grp.Data ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) Saldo
    into #extrato
    from (
        select id
             , datamovimento data
             , valor * case 
                    --when AgendamentoItemId is not null then 0
                    when TipoMovimento = 1 then 1 
                    when TipoMovimento = 0 then -1 
               end Valor
             , descricao
             , mov.ContaBancariaId
             , null ValorSaldo
        from movimentobancario mov

        union
        select ag.Id
             , coalesce(agi.DataPagamento, agi.Data, ag.DataInicial) Data
             , coalesce(agi.ValorPago, agi.Valor, ag.Valor) * case 
                    when mov.Id is not null then 0
                    when ag.Tipo = 1 then 1
                    when ag.Tipo = 0 then -1
               end Valor
             , ag.descricao
             , coalesce(agi.ContaBancariaId, ag.ContaBancariaId) ContaBancariaId
             , null ValorSaldo
        from Agendamento ag
        inner join AgendamentoItem agi on agi.AgendamentoId = ag.Id
        left join MovimentoBancario mov on mov.AgendamentoItemId = agi.Id
        
        union
        select id
             , DATEADD(day, -1, min(conta.Data)) Data
             , conta.ValorSaldoInicial + sum(isnull(conta.valor, 0)) Valor
             , 'SALDO INICIAL' descricao
             , conta.Id
             , null Saldo
        from (
            select conta.id
                 , coalesce(agi.DataPagamento, agi.Data, conta.DataSaldoInicial) Data
                 --, conta.DataSaldoInicial Data
                 , conta.valorsaldoinicial
                 , mov.valor * case when mov.TipoMovimento = 0 then 1 else -1 end Valor
            from ContaBancaria conta
            left join MovimentoBancario mov on mov.ContaBancariaId = conta.Id and mov.DataMovimento < conta.DataSaldoInicial
            left join AgendamentoItem agi on agi.ContaBancariaId = conta.Id and coalesce(agi.DataPagamento, agi.Data) < conta.DataSaldoInicial
            left join Agendamento ag on ag.Id = agi.AgendamentoId
        )conta
        group by conta.Id, conta.ValorSaldoInicial
    )grp
    inner join ContaBancaria conta on conta.id = grp.ContaBancariaId;


    /*------------------------------+
    | MONTAGEM DOS EXTRATOS DIÁRIOS |
    +------------------------------*/

    select cast(row_number() over (partition by ext.ContaBancariaId, substring(convert(nvarchar(8), ext.Data, 112), 1, 6) order by ext.ContaBancariaId, ext.Data) as int) idx
         , ext.ContaBancariaId, ext.ContaBancariaNome, ext.Data, ext.Saldo
    into #extratoDiario
    from #extrato ext
    inner join (
        select max(Idx) Idx, ContaBancariaId, data
        from #extrato
        group by ContaBancariaId, data
    )tmp on ext.Idx = tmp.Idx and ext.ContaBancariaId = tmp.ContaBancariaId and ext.Data = tmp.Data
    order by ext.Data, ext.ContaBancariaId;


    /*-------------------------------------+
    | MONTAGEM DOS EXTRATOS INTERMEDIÁRIOS |
    +-------------------------------------*/

    drop table if exists #ExtratoIntermediario;

    select distinct Data, Id ContaBancariaId
    into #ExtratoIntermediario
    from #Extrato, contabancaria
    order by 1, 2;

    alter table #ExtratoIntermediario add Saldo decimal(18,6) null;
    
    update #ExtratoIntermediario set saldo = ext.Saldo
    from #ExtratoIntermediario tmp
    inner join #ExtratoDiario ext on ext.Data = tmp.Data
                                  and ext.ContaBancariaId = tmp.ContaBancariaId;

    update #ExtratoIntermediario set saldo = ant.Saldo
    from #ExtratoIntermediario tmp
    inner join (
        select tmp.Data, tmp.ContaBancariaId
             , case 
                when tmp.Saldo is not null then tmp.Saldo
                else (

        select coalesce(intermed.Saldo, 0) Saldo
        from (     
            select interno.ContaBancariaId, max(interno.Data) Data
            from #ExtratoIntermediario interno
            where interno.ContaBancariaId = tmp.ContaBancariaId
              and saldo is not null
              and interno.Data < tmp.Data
            group by interno.ContaBancariaId
        )interno
        inner join #ExtratoIntermediario intermed on intermed.Data = interno.Data
                                                  and intermed.ContaBancariaId = interno.ContaBancariaId

            ) end Saldo
        from #ExtratoIntermediario tmp
    ) ant on ant.Data = tmp.Data
         and ant.ContaBancariaId = tmp.ContaBancariaId
    where tmp.saldo is null;

    update #ExtratoIntermediario set saldo = 0 where saldo is null;

    insert into #ExtratoDiario
    /*select -1 idx, tmp.ContaBancariaId, conta.Nome, tmp.Data, 'SALDO AUTO' Descricao, 0 Valor, tmp.Saldo*/
    select -1 idx, tmp.ContaBancariaId, conta.Nome, tmp.Data, tmp.Saldo
    from #ExtratoIntermediario tmp
    inner join ContaBancaria conta on conta.Id = tmp.ContaBancariaId
    left join #ExtratoDiario ext on ext.Data = tmp.Data
                                 and ext.ContaBancariaId = tmp.ContaBancariaId
    where ext.Data is null and ext.ContaBancariaId is null
    order by tmp.data, tmp.contabancariaid


    /*------------------------------+
    | MONTAGEM DOS EXTRATOS MENSAIS |
    +------------------------------*/
    select distinct ext.idx, ext.ContaBancariaId, ext.ContaBancariaNome, grp.MesAno, ext.Saldo
    into #extratoMensal
    from #extratoDiario ext
    inner join (
        select max(idx) idx
             , ContaBancariaId
             , ContaBancariaNome
             , substring(convert(nvarchar(8), Data, 112), 1, 6) MesAno
        from #extratoDiario
        group by ContaBancariaId, ContaBancariaNome, substring(convert(nvarchar(8), Data, 112), 1, 6)
    ) grp on grp.idx = ext.idx 
         and grp.ContaBancariaId = ext.ContaBancariaId
         and grp.MesAno = substring(convert(nvarchar(8), ext.Data, 112), 1, 6)
    order by grp.MesAno, ext.ContaBancariaId, ext.idx, ext.ContaBancariaNome, ext.Saldo;
    
    insert into #extratoDiario
    select 1 idx, -1 ContaBancariaId, 'Todas' ContaBancariaNome, data, sum(Saldo) Saldo
    from #extratoDiario
    group by data
    order by data;

    insert into #extratoMensal
    select 1 idx, -1 ContaBancariaId, 'Todas' ContaBancariaNome, MesAno, sum(Saldo) Saldo
    from #extratoMensal
    group by MesAno
    order by MesAno;
";

            using var ctx = _contexto;
            ctx.Database.OpenConnection();

            await ctx.Database.ExecuteSqlRawAsync(cmdSql);

            var filtroMensal = $"where MesAno between '{inicio:yyyyMM}' and '{fim:yyyyMM}'";
            var filtroDiario = $"where data between '{inicio:yyyyMMdd}' and '{fim:yyyyMMdd}'";

            var extratoDiario = await ctx.ExtratoDiario
                .FromSqlRaw($"select * from #extratoDiario {filtroDiario} order by Data, ContaBancariaId")
                .AsNoTracking()
                .ToListAsync();

            var extratoMensal = await ctx.ExtratoMensal
                .FromSqlRaw($"select * from #extratoMensal {filtroMensal} order by MesAno, ContaBancariaId")
                .AsNoTracking()
                .ToListAsync();

            ctx.Database.CloseConnection();

            return new ExtratoRelatorio(extratoDiario, extratoMensal);
        }
    }
}
