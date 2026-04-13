select
    po.SettlementAccountId as SettlementAccountId,
    Sum(iif(po.Direction = @incomingDirection, po.Sum, 0)) as IncomingSum,
    Sum(iif(po.Direction = @outgoingDirection, po.Sum, 0)) as OutgoingSum
from Accounting_PaymentOrder po
where po.FirmId = @firmId
    and po.Date >= @balanceDate and po.Date <= @date
    and po.OperationType in @currencyOperationsTypes
    and coalesce(po.OperationState, @operationStateDefault) not in @badStates
group by po.SettlementAccountId;