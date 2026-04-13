select
    b.Id as DocumentBaseId,
    iif(
            coalesce(po.OperationState, @operationStateDefault) not in @badStates
--PaidStatusFilter--                and po.PaidStatus = @paidStatus
--PassedOutsourcingCheckFilter--                and coalesce(po.OutsourceState, 0) not in @badOutsourceState
            ,1, 0
    ) as IsValid
from #BaseIds as b
join dbo.Accounting_PaymentOrder as po on po.DocumentBaseId = b.Id
join dbo.SettlementAccount as sa on sa.id = po.SettlementAccountId