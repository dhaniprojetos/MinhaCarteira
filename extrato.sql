--select * from ##extratoDiario order by data, contabancariaid
--select * from ##extratodiario where contabancariaid in (3) order by data    

    drop table if exists ##extrato;
    drop table if exists ##extratoDiario;
    drop table if exists ##extratoMensal;
    
    select row_number() over(partition by grp.ContaBancariaId, grp.Data order by grp.Data) Idx
         , grp.ContaBancariaId
         , CONVERT(DATE, grp.Data) Data
         , grp.Descricao
         , grp.Valor
         , sum(grp.Valor) over (partition by grp.ContaBancariaId order by grp.Data ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) Saldo
    into ##extrato
    from (
        select datamovimento data
             , valor * case 
             --when AgendamentoItemId is not null then 0
                    when TipoMovimento = 1 then 1 
                    when TipoMovimento = 0 then -1 
               end Valor
             , descricao
             , mov.ContaBancariaId
             , null ValorSaldo
        from movimentobancario mov

        --union
        --select coalesce(agi.DataPagamento, agi.Data, ag.DataInicial) Data
        --     , coalesce(agi.ValorPago, agi.Valor, ag.Valor) * case 
        --            when ag.Tipo = 1 then 1
        --            when ag.Tipo = 0 then -1
        --       end Valor
        --     , ag.descricao
        --     , coalesce(agi.ContaBancariaId, ag.ContaBancariaId) ContaBancariaId
        --     , null ValorSaldo
        --from Agendamento ag
        --inner join AgendamentoItem agi on agi.AgendamentoId = ag.Id
        
        union
        select DATEADD(day, -1, min(conta.Data)) Data
             , conta.ValorSaldoInicial + sum(isnull(conta.valor, 0)) Valor
             , 'SALDO INICIAL' descricao
             , conta.Id
             , null Saldo
        from (
            select conta.id
                --, coalesce(agi.DataPagamento, agi.Data, conta.DataSaldoInicial) Data
                 , conta.DataSaldoInicial Data
                 , conta.valorsaldoinicial
                 , mov.valor * case when mov.TipoMovimento = 0 then 1 else -1 end Valor
            from ContaBancaria conta
            left join MovimentoBancario mov on mov.ContaBancariaId = conta.Id and mov.DataMovimento < conta.DataSaldoInicial
            --left join AgendamentoItem agi on agi.ContaBancariaId = conta.Id and coalesce(agi.DataPagamento, agi.Data) < conta.DataSaldoInicial
            --left join Agendamento ag on ag.Id = agi.AgendamentoId
        )conta
        group by conta.Id, conta.ValorSaldoInicial
    )grp;

    select ext.*
    into ##extratoDiario
    from ##extrato ext
    inner join (
        select max(Idx) Idx, ContaBancariaId, data
        from ##extrato
        group by ContaBancariaId, data
    )tmp on ext.Idx = tmp.Idx and ext.ContaBancariaId = tmp.ContaBancariaId and ext.Data = tmp.Data
    order by ext.Data, ext.ContaBancariaId;
    
    select row_number() over (partition by ext.ContaBancariaId, ext.MesAno order by ext.MesAno) Idx
         , ext.ContaBancariaId
         , ext.MesAno
         , ext.Descricao
         , ext.Valor
         , ext.Saldo
    into ##extratoMensal
    from (
        select ContaBancariaId
             , substring(convert(nvarchar(8), Data, 112), 1, 6) MesAno
             , Descricao
             , Valor
             , Saldo 
        from ##extratoDiario
    ) ext;
