select p.DocumentBaseId,
       p.Sum
from
    (select
        op.DocumentBaseId,
        op.Sum
        from	dbo.Accounting_PaymentOrder as op
            join dbo.AccountingLinkedDocumentBase as ldb on
                ldb.Id = op.DocumentBaseId            				
        where op.FirmId = @firmId
            and coalesce(op.OperationState, @OperationStateDefault) not in (select Id from #BadStates)
            and op.OperationType in (select Id from #OperationTypes)
            and op.Date between @startDate and @endDate            
    union all
    select
        op.DocumentBaseId,
        op.Sum
        from	dbo.Accounting_CashOrder			op
            join dbo.AccountingLinkedDocumentBase as ldb on
                ldb.Id = op.DocumentBaseId
        where op.FirmId = @firmId
            and op.OperationType in (select Id from #OperationTypes)
            and op.Date between @startDate and @endDate
    ) p
