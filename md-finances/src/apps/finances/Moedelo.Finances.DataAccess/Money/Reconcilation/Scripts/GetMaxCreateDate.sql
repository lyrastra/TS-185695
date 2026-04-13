select
	max(CreateDate)
	from dbo.BalanceReconcilation
	where FirmId = @firmId
		and SettlementAccountId = @settlementAccountId;