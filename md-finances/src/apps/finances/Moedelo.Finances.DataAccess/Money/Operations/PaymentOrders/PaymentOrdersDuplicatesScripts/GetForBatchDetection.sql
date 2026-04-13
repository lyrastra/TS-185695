select
	po.Id,
	po.DocumentBaseId,
	po.Direction,
	po.Date,
	po.Sum,
	po.Description,
	po.PaymentNumber																	as Number,
	case
		when po.Direction = @OutgoingDirection then
			isnull(po.PaymentSnapshot.value('(/PaymentOrder/Recipient/SettlementNumber/node())[1]', 'varchar(20)'), '')
		when po.Direction = @IncomingDirection then
			isnull(po.PaymentSnapshot.value('(/PaymentOrder/Payer/SettlementNumber/node())[1]', 'varchar(20)'), '')
		else ''
	end																					as ContractorSettlementAccount,
	case
		when po.Direction = @OutgoingDirection then
			isnull(po.PaymentSnapshot.value('(/PaymentOrder/Recipient/Inn/node())[1]', 'varchar(12)'), '')
		when po.Direction = @IncomingDirection then
			isnull(po.PaymentSnapshot.value('(/PaymentOrder/Payer/Inn/node())[1]', 'varchar(12)'), '')
		else ''
	end																					as ContractorInn,
	iif(po.OperationType = @PaymentOrderOutgoingForTransferSalaryOperationType, 1, 0)	as IsSalaryOperation,
	iif(po.OperationType = @PaymentOrderOutgoingProfitWithdrawingOperationType, 1, 0)	as IsProfitWithdrawingOperation,
	po.OperationType

from dbo.Accounting_PaymentOrder as po
	where po.FirmId = @FirmId
		and po.SettlementAccountId = po.SettlementAccountId
		and po.Date between @StartDate and @EndDate
		and isnull(po.OperationState, @RegularOperationState)not in (select Id from @BadOperationStates)
	order by po.Id;