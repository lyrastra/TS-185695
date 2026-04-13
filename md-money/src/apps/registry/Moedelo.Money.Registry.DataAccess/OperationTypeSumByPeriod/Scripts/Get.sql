select 
    po.Direction,
    po.OperationType as [Type],
    datepart(year, po.Date) as OperationDateYear,
    datepart(month, po.Date) as OperationDateMonth,
    sum(case po.OperationType
            when @OperationTypePaymentOrderIncomingCurrencyPurchase then po.TotalSum
            when @OperationTypePaymentOrderOutgoingCurrencySale then po.TotalSum
            when @OperationTypePaymentOrderIncomingCurrencyOther then po.TotalSum
            when @OperationTypeCurrencyBankFee then po.TotalSum
            when @OperationTypePaymentOrderOutgoingCurrencyPaymentToSupplier then po.TotalSum
            when @OperationTypePaymentOrderOutgoingCurrencyOther then po.TotalSum
            when @OperationTypePaymentOrderIncomingCurrencyPaymentFromCustomer then po.TotalSum
            else po.Sum
        end) as OperationSum,
    datepart(year, po.AcquiringCommissionDate) as AcquiringCommissionDateYear,
    datepart(month, po.AcquiringCommissionDate) as AcquiringCommissionDateMonth,
    sum(po.AcquiringCommission) as AcquiringCommissionSum,
    null as PaidCardSum
from dbo.Accounting_PaymentOrder po
where po.FirmId = @firmId

--OperationDateFilter-- and po.Date between @startDate and @endDate
--OperationOrAcquiringCommissionDateFilter-- and ( po.Date between @startDate and @endDate or po.AcquiringCommissionDate between @startDate and @endDate)
--OperationTypes-- and po.OperationType in (select Id from #OperationTypes)

and po.PaidStatus = @PayedStatus
and coalesce(po.OperationState, @OperationStateDefault) not in (select Id from #BadStates)
and po.OutsourceState is null
group by 
    po.Direction,
    po.OperationType,
    datepart(year, po.Date),
    datepart(month, po.Date),
    datepart(year, po.AcquiringCommissionDate),
    datepart(month, po.AcquiringCommissionDate)

union all

select 
    co.Direction,
    co.OperationType as [Type],
    datepart(year, co.Date) as OperationDateYear,
    datepart(month, co.Date) as OperationDateMonth,
    sum(co.Sum) as OperationSum,
    null as AcquiringCommissionDateYear,
    null as AcquiringCommissionDateMonth,
    null as AcquiringCommissionSum,
    sum(co.PaidCardSum) as PaidCardSum
from dbo.Accounting_CashOrder co
where co.FirmId = @firmId
and co.Date between @startDate and @endDate

--OperationTypes-- and po.OperationType in (select Id from #OperationTypes)
group by 
    co.Direction,
    co.OperationType,
    datepart(year, co.Date),
    datepart(month, co.Date)

union all

select
    po.Direction,
    po.OperationType as [Type],
    datepart(year, po.DocumentDate) as OperationDateYear,
    datepart(month, po.DocumentDate) as OperationDateMonth,
    sum(po.Sum) as OperationSum,
    null as AcquiringCommissionDateYear,
    null as AcquiringCommissionDateMonth,
    null as AcquiringCommissionSum,
    null as PaidCardSum
from docs.PurseOperation po
where po.FirmId = @firmId
and po.DocumentDate between @startDate and @endDate

--OperationTypes-- and po.OperationType in (select Id from #OperationTypes)
group by 
    po.Direction,
    po.OperationType,
    datepart(year, po.DocumentDate),
    datepart(month, po.DocumentDate)
;