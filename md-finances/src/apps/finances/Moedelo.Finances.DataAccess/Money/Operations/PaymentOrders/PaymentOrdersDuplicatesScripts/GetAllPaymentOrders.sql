select
	po.Id,
	po.DocumentBaseId,
	po.Direction,
	po.Description,
	po.SettlementAccountId,
	po.KontragentId,
	po.PaymentNumber																							as Number,
	isnull(po.PaymentSnapshot.value('(/PaymentOrder/Payer/SettlementNumber/node())[1]', 'varchar(20)'), '')		as PayerSettlementNumber,
	isnull(po.PaymentSnapshot.value('(/PaymentOrder/Payer/Inn/node())[1]', 'varchar(12)'), '')					as PayerInn,
	isnull(po.PaymentSnapshot.value('(/PaymentOrder/Recipient/SettlementNumber/node())[1]', 'varchar(20)'), '') as RecipientSettlementNumber,
	isnull(po.PaymentSnapshot.value('(/PaymentOrder/Recipient/Inn/node())[1]', 'varchar(12)'), '')				as RecipientInn,
	iif(po.OperationType = @PaymentOrderOutgoingForTransferSalaryOperationType, 1, 0)							as IsSalaryOperation
	from dbo.Accounting_PaymentOrder as po
	where po.FirmId = @FirmId
		and po.Sum = @Sum
		and po.Direction = @Direction
		and po.Date between @StartDate and @EndDate
		and isnull(po.OperationState, @RegularOperationState)not in (select Id from @BadOperationStates);
