select
    co.DocumentBaseId,
    co.Number,
    co.Date,
    co.Sum,
    co.OperationType as Type
from Accounting_CashOrder as co
where co.FirmId = @firmId
  and co.OperationType in (select Id from #operationTypes)
  --StartDateFilter-- and co.Date >= @startDate
  and co.Date <= @endDate
order by co.Id offset @offset rows fetch next @limit rows only;