select top 1
	SessionId
	from dbo.BalanceReconcilation
	where FirmId = @FirmId
		and SettlementAccountId = @SettlementAccountId
		and Status = @InProgressStatus
		and CreateDate > @DayAgo
	order by id desc;
