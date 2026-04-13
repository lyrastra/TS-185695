update
	dbo.BalanceReconcilation
	set
	Status = @completeStatus
	where FirmId = @FirmId
		and Status = @readyStatus
		and SettlementAccountId = @settlementAccountId;