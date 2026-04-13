select
	DocumentBaseId,
	OperationType
	from dbo.Accounting_CashOrder
	where FirmId = @FirmId
		and DocumentBaseId in (select Id from #BaseIds);