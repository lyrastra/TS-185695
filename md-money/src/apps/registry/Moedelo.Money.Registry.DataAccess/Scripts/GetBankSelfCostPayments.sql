select
    op.DocumentBaseId,
    op.PaymentNumber as Number,
    op.Date,
    op.Sum,
    op.OperationType as Type,
    op.TotalSum as RubSum
from Accounting_PaymentOrder as op
where op.FirmId = @firmId
  and coalesce(op.OperationState, @operationStateDefault) not in (select Id from #badStates)
  and op.PaidStatus = @paidStatus
  and op.OperationType in (select Id from #operationTypes)
  --StartDateFilter-- and op.Date >= @startDate
  and op.Date <= @endDate
order by op.Id offset @offset rows fetch next @limit rows only;
