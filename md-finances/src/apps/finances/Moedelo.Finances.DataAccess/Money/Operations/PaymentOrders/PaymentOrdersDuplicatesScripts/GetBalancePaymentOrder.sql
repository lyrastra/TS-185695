select
	po.Id,
	po.DocumentBaseId,
	po.Date,
	po.Sum
	from dbo.Accounting_PaymentOrder as po
	where po.FirmId = @FirmId
		and po.OperationType = @balanceOperationType
		and po.SettlementAccountId = @settlementAccountId;