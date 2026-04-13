select
--TopConstraint-- top(@TopConstraint)
po.Id,
po.FirmId,
po.DocumentBaseId,
po.Date,
po.PaymentNumber as Number,
po.Direction,
po.SettlementAccountId,
po.KontragentSettlementAccountId as TransferSettlementAccountId,
po.KontragentId,
po.KontragentName,
po.SalaryWorkerId as WorkerId,
po.OperationType,
po.OrderType,
po.OperationState,
po.PaidStatus,
po.Sum,
po.Description,
po.PaymentPriority,
po.PaymentSnapshot,
po.IncludeNds,
po.NdsSum,
po.NdsType,
po.UnderContract,
po.SourceFileId,
po.KbkId,
--SelectKbkNumberFromSnapshot-- po.PaymentSnapshot.value('(/PaymentOrder/Kbk/node())[1]', 'varchar(25)') as KbkNumber,
    /*--KbkJoin--
    kbk.KbkPaymentType,
    kbk.KbkType,
    (case when kbk.KbkPaymentType = 0 then 1 else 0 end) as IsPayment,
    --KbkJoin--*/
po.BudgetaryTaxesAndFees,
po.BudgetaryPeriodNumber,
po.BudgetaryPeriodType,
po.BudgetaryPeriodYear,
po.PatentId
from dbo.Accounting_PaymentOrder as po
/*--BaseIdsJoin--
join @BaseIds as ids
	on ids.Id = po.DocumentBaseId
--BaseIdsJoin--*/
/*--KbkJoin--
left join dbo.KbkNumber as kbk
	on kbk.Id = po.KbkId
--KbkJoin--*/
where 1=1
--FirmIdFilter-- and po.FirmId = @FirmId
--OperationType-- and po.OperationType in (select Id from @OperationsType)
--PeriodFilter-- and po.Date between @StartDate and @EndDate
--PaidStatus-- and po.PaidStatus = @PaidStatus
--PaymentDirection-- and po.Direction = @PaymentDirection
--ExcludeOperationStates-- and coalesce(po.OperationState, 0) not in (select Id from @ExcludeOperationStates)
--PatentId-- and po.PatentId = @PatentId

/*Фильтры для бюджетных платежей начало*/
--KbkFilter-- and po.KbkId = @KbkId
--KbkPaymentType-- and coalesce(kbk.KbkPaymentType, 0) = @KbkPaymentType
--BudgetaryTaxesAndFees-- and po.BudgetaryTaxesAndFees in (select Id from @BudgetaryTaxesAndFees)
--BudgetaryPeriodType-- and po.BudgetaryPeriodType = @BudgetaryPeriodType
--BudgetaryPeriodNumber-- and po.BudgetaryPeriodNumber = @BudgetaryPeriodNumber
--BudgetaryPeriodYear-- and po.BudgetaryPeriodYear = @BudgetaryPeriodYear and po.BudgetaryPeriodType != 8 -- NoPeriod - Без периода
/*Фильтры для бюджетных платежей конец*/
