/*
declare @FirmId int = 7494;
declare @OperationType int = 39;
declare @KbkId int = 56;
declare @PaidStatus int = 6;
declare @BudgetaryAccountCode int = 680200;
declare @Year int = 2023;
declare @PeriodType int = 3; -- квартал
declare @PeriodNumber int = 1;
declare @UnifiedOperationType int = 158; -- Исходящее П/П: Единый налоговый платеж (aka Бюджетный платеж)
declare @UnifiedBudgetaryAccountCode int = 686900;
*/

select 
    po.DocumentBaseId
    , po.Date
    , po.Sum
    , po.BudgetaryPeriodType as PeriodType
    , po.BudgetaryPeriodNumber as PeriodNumber
from dbo.Accounting_PaymentOrder po
where 
    po.FirmId = @FirmId
    and po.KbkId = @KbkId
    and po.BudgetaryTaxesAndFees = @BudgetaryAccountCode
    and po.OperationType = @OperationType
    and po.PaidStatus = @PaidStatus
    and (po.OperationState is null or po.OperationState not in @BadStates)
    and po.BudgetaryPeriodYear = @Year
    --PeriodFilter-- and (po.BudgetaryPeriodType = @PeriodType and po.BudgetaryPeriodNumber = @PeriodNumber)
union all
select 
    po.DocumentBaseId
    , po.Date
    , utp.PaymentSum as Sum
    , utp.PeriodType as PeriodType
    , utp.PeriodNumber as PeriodNumber
from dbo.Accounting_PaymentOrder po
    join dbo.UnifiedTaxPayment utp
            on utp.ParentDocumentId = po.DocumentBaseId
where 
    po.FirmId = @FirmId
    and po.BudgetaryTaxesAndFees = @UnifiedBudgetaryAccountCode
    and po.OperationType = @UnifiedOperationType
    and po.PaidStatus = @PaidStatus
    and (po.OperationState is null or po.OperationState not in @BadStates)
    and utp.KbkNumberId = @KbkId
    and utp.PeriodYear = @Year
    --PeriodFilter-- and (utp.PeriodType = @PeriodType and utp.PeriodNumber = @PeriodNumber)
;