select Id,
	ServiceBalance,
	BankBalance,
	ReconcilationDate,
	CreateDate,
	SessionId,
	Status,
	SettlementAccountId
	from dbo.BalanceReconcilation 
	where FirmId = @FirmId
	and SessionId = @SessionId;
