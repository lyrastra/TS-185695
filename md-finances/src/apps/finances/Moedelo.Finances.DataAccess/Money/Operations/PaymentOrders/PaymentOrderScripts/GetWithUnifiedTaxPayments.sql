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
	poKbk.KbkPaymentType,
	poKbk.KbkType,
	(case when poKbk.KbkPaymentType = 0 then 1 else 0 end) as IsPayment,
	--KbkJoin--*/
	po.BudgetaryTaxesAndFees,
	po.BudgetaryPeriodNumber,
	po.BudgetaryPeriodType,
	po.BudgetaryPeriodYear,
	po.PatentId,
	po.TradingObjectId,
	utp.ParentDocumentId,
	utp.DocumentBaseId,
	utp.KbkNumberId as KbkId,
	utpKbk.Kbk as KbkNumber,
	utpKbk.KbkType as KbkType,
	utpKbk.AccountCode,
	utp.PeriodNumber,
	utp.PeriodType,
	utp.PeriodYear,
	utp.PatentId,
	utp.TradingObjectId,
	utp.PaymentSum as Sum
from
	dbo.Accounting_PaymentOrder po
		left join dbo.UnifiedTaxPayment utp
			on utp.ParentDocumentId = po.DocumentBaseId
/*--BaseIdsJoin--
		join @BaseIds as ids
			on ids.Id = po.DocumentBaseId
--BaseIdsJoin--*/
/*--KbkJoin--
		left join dbo.KbkNumber as poKbk
			on poKbk.Id = po.KbkId
--KbkJoin--*/
/*--UtpKbkJoin--
		left join dbo.KbkNumber utpKbk
			on utpKbk.Id = utp.KbkNumberId
--KbkJoin--*/
where 1=1
--FirmIdFilter-- and po.FirmId = @FirmId
--OperationType-- and po.OperationType in (select Id from @OperationsType)
--PeriodFilter-- and po.Date between @StartDate and @EndDate
--PaidStatus-- and po.PaidStatus = @PaidStatus
--PaymentDirection-- and po.Direction = @PaymentDirection
--ExcludeOperationStates-- and coalesce(po.OperationState, 0) not in (select Id from @ExcludeOperationStates)
--PatentId-- and isnull(utp.PatentId, po.PatentId) = @PatentId
--KbkFilter-- and isnull(utp.KbkNumberId, po.KbkId) = @KbkId
--KbkPaymentType-- and ((isnull(utpKbk.KbkPaymentType, 0) = @KbkPaymentType and utp.DocumentBaseId is not null) or (isnull(poKbk.KbkPaymentType, 0) = @KbkPaymentType) and utp.DocumentBaseId is null)
--BudgetaryTaxesAndFees-- and isnull(utpKbk.AccountCode, po.BudgetaryTaxesAndFees) in (select Id from @BudgetaryTaxesAndFees)
--BudgetaryPeriodType-- and isnull(utp.PeriodType, po.BudgetaryPeriodType) = @BudgetaryPeriodType
--BudgetaryPeriodNumber-- and isnull(utp.PeriodNumber, po.BudgetaryPeriodNumber) = @BudgetaryPeriodNumber
--BudgetaryPeriodYear-- and (utp.PeriodYear = @BudgetaryPeriodYear or (po.BudgetaryPeriodYear = @BudgetaryPeriodYear and po.BudgetaryPeriodType != 8)) -- NoPeriod - Без периода
