select top 1
	Id,
	SettlementAccountId,
	ServiceBalance,
	BankBalance,
	ReconcilationDate,
	CreateDate,
	SessionId,
	Status
	from dbo.BalanceReconcilation
	where FirmId = @FirmId
		and Status = @ReadyStatus
	order by id;
