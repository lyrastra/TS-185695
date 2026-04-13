select
    po.PaymentNumber as Number,
    po.Date,
    isnull(utp.PaymentSum, po.Sum) as Sum,
    isnull(utp.DocumentBaseId, po.DocumentBaseId) as DocumentBaseId
from dbo.Accounting_PaymentOrder as po
    left join dbo.UnifiedTaxPayment as utp
        on utp.ParentDocumentId = po.DocumentBaseId
where po.FirmId = @FirmId
    and po.OperationType in @OperationTypes
    --QueryByNumberFilter-- and po.PaymentNumber like '%' +@QueryByNumber + '%'
    --StartDateFilter-- and po.Date >= @StartDate
    --EndDateFilter-- and po.Date <= @EndDate
    and isnull(utp.KbkNumberId, po.KbkId) in @KbkIds
    and po.PaidStatus = @PaidStatus
    and BudgetaryTaxesAndFees in @BudgetaryAccountCodes
    and (po.OperationState is null or OperationState not in @BadStates)
    order by po.Id asc
    offset @Offset rows fetch next @Limit rows only;