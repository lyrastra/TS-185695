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
	co.PatentId,
    utp.ParentDocumentId,
    utp.DocumentBaseId,
    utp.KbkNumberId as KbkId,
/*--UtpKbkJoin--
    utpKbk.Kbk as KbkNumber,
    utpKbk.KbkType as KbkType,
    utpKbk.AccountCode,
--KbkJoin--*/ 
    utp.PeriodNumber,
    utp.PeriodType,
    utp.PeriodYear,
    utp.PatentId,
    utp.PaymentSum as Sum
from dbo.Accounting_CashOrder co
    left join dbo.UnifiedTaxPayment utp  on utp.ParentDocumentId = co.DocumentBaseId

/*--KbkJoin--
    left join dbo.KbkNumber as kbk on kbk.Id = co.KbkId
--KbkJoin--*/
/*--UtpKbkJoin--
	left join dbo.KbkNumber utpKbk on utpKbk.Id = utp.KbkNumberId
--KbkJoin--*/
where co.FirmId = @FirmId
and co.OperationType in (select Id from @OperationsType)
--PeriodFilter-- and co.Date between @StartDate and @EndDate
--PaymentDirection-- and co.Direction = @PaymentDirection

--PatentId-- and isnull(utp.PatentId, co.PatentId) = @PatentId 
--KbkFilter-- and isnull(utp.KbkNumberId, co.KbkId) = @KbkId 
--KbkPaymentType-- and (isnull(utpKbk.KbkPaymentType, 0) = @KbkPaymentType or isnull(kbk.KbkPaymentType, 0) = @KbkPaymentType)
--BudgetaryTaxesAndFees-- and isnull(utpKbk.AccountCode, co.BudgetaryTaxesAndFees) in (select Id from @BudgetaryTaxesAndFees)
--BudgetaryPeriodType-- and isnull(utp.PeriodType, co.BudgetaryPeriodType) = @BudgetaryPeriodType
--BudgetaryPeriodNumber-- and isnull(utp.PeriodNumber, co.BudgetaryPeriodNumber) = @BudgetaryPeriodNumber
--BudgetaryPeriodYear-- and (utp.PeriodYear = @BudgetaryPeriodYear or (co.BudgetaryPeriodYear = @BudgetaryPeriodYear and co.BudgetaryPeriodType != 8)) -- NoPeriod - Без периода