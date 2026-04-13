DECLARE @outgoingDirection INT = 1;

select
	isnull(try_cast(PaymentNumber as int), 0) Number
from 
	dbo.Accounting_PaymentOrder
where 
	FirmId = @firmId
	and Date >= @startOfYear and Date <= @endOfYear
	and SettlementAccountId = @settlementAccountId
	and Direction = @outgoingDirection
	and coalesce(OperationState, @operationStateDefault) not in @badStates
	and isnull(try_cast(PaymentNumber as int), 0) > @cut
order by
	Number asc

		