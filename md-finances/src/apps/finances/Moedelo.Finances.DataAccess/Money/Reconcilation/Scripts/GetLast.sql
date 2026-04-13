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
	    /*--FilterBySettlementAccountId--and SettlementAccountId = @SettlementAccountId--FilterBySettlementAccountId--*/
	order by Id desc;
