/*
declare @FirmId int = 7778168
declare @OperationType int = 126 -- Исходящий кассовый ордер: Бюджетный платеж (CashOrderBudgetaryPayment = 126)

declare @StartDate datetime = '20190101'
declare @EndDate datetime = '20191231'
declare @KbkId int = 190
declare @KbkPaymentType int = 0 -- Взносы/Налоги
declare @PaymentDirection int = 1 -- Исходящий платёж (Outgoing = 1)
declare @BudgetaryTaxesAndFees int = 690201
declare @BudgetaryPeriodType int = 1 -- Годовой (Year = 1)
declare @BudgetaryPeriodYear int = 2019
declare @PatentId bigint = null
*/

select
    co.Id,
    co.FirmId,
    co.DocumentBaseId,
    co.Date,
    co.Number,
    co.Direction,
    co.OperationType,
    co.Sum,
    co.Destination,
    co.KbkId,
    /*--KbkJoin--
    kbk.Kbk as KbkNumber,
    kbk.KbkPaymentType,
    kbk.KbkType,
    (case when kbk.KbkPaymentType = 0 then 1 else 0 end) as IsPayment,
    --KbkJoin--*/
    co.BudgetaryTaxesAndFees,
    co.BudgetaryPeriodNumber,
    co.BudgetaryPeriodType,
    co.BudgetaryPeriodYear,
    co.PatentId
from dbo.Accounting_CashOrder as co
/*--KbkJoin--
left join dbo.KbkNumber as kbk
	on kbk.Id = co.KbkId
--KbkJoin--*/
where co.FirmId = @FirmId
  and co.OperationType in (select Id from @OperationsType)
--PeriodFilter-- and co.Date between @StartDate and @EndDate
--KbkFilter-- and co.KbkId = @KbkId
--KbkPaymentType-- and coalesce(kbk.KbkPaymentType, 0) = @KbkPaymentType
--PaymentDirection-- and co.Direction = @PaymentDirection
--BudgetaryTaxesAndFees-- and co.BudgetaryTaxesAndFees in (select Id from @BudgetaryTaxesAndFees)
--BudgetaryPeriodType-- and co.BudgetaryPeriodType = @BudgetaryPeriodType
--BudgetaryPeriodYear-- and co.BudgetaryPeriodYear = @BudgetaryPeriodYear and co.BudgetaryPeriodType != 8 -- NoPeriod - Без периода
--PatentId-- and co.PatentId = @PatentId
