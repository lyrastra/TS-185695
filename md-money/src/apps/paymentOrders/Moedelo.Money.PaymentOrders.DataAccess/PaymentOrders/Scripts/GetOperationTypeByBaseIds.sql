select
	DocumentBaseId,
	OperationType
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and DocumentBaseId in (select Id from #BaseIds);