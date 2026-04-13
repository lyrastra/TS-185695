select
	DocumentBaseId
from 
	dbo.Accounting_PaymentOrder
where 
	FirmId = @FirmId
	and OperationType = @operationType
	and Date >= @startDate and Date <= @endDate
	and coalesce(OperationState, @operationStateDefault) not in @badStates
;