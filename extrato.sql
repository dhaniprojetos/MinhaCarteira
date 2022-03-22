    select tmp.ContaBancariaId
         , tmp.Data
         , tmp.Descricao
         , tmp.Valor
         , sum(tmp.Valor) over (partition by tmp.ContaBancariaId order by tmp.Data ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) Saldo
    from (
        select datamovimento data
             , valor * case 
                    when AgendamentoItemId is not null then 0
                    when TipoMovimento = 1 then 1 
                    when TipoMovimento = 0 then -1 
               end Valor
             , descricao
             , mov.ContaBancariaId
             , null ValorSaldo
        from movimentobancario mov

        union
        select coalesce(agi.DataPagamento, agi.Data, ag.DataInicial) Data
             , coalesce(agi.ValorPago, agi.Valor, ag.Valor) * case 
                    when ag.Tipo = 1 then 1
                    when ag.Tipo = 0 then -1
               end Valor
             , ag.descricao
             , coalesce(agi.ContaBancariaId, ag.ContaBancariaId) ContaBancariaId
             , null ValorSaldo
        from Agendamento ag
        inner join AgendamentoItem agi on agi.AgendamentoId = ag.Id
        
        union
        select DATEADD(day, -1, min(conta.Data)) Data
             , conta.ValorSaldoInicial + sum(isnull(conta.valor, 0)) Valor
             , 'SALDO INICIAL' descricao
             , conta.Id
             , null Saldo
        from (
            select conta.id
                 , coalesce(agi.DataPagamento, agi.Data, conta.DataSaldoInicial) Data
                 , conta.valorsaldoinicial
                 , coalesce(agi.valorpago, agi.valor, mov.valor) * case when ag.Tipo = 0 then 1 else -1 end Valor
            from ContaBancaria conta
            left join MovimentoBancario mov on mov.ContaBancariaId = conta.Id and mov.DataMovimento < conta.DataSaldoInicial
            left join AgendamentoItem agi on agi.ContaBancariaId = conta.Id and coalesce(agi.DataPagamento, agi.Data) < conta.DataSaldoInicial
            left join Agendamento ag on ag.Id = agi.AgendamentoId
        )conta
        group by conta.Id, conta.ValorSaldoInicial
    )tmp
    where tmp.ContaBancariaId in (2)
    --group by tmp.contabancariaid, tmp.data
