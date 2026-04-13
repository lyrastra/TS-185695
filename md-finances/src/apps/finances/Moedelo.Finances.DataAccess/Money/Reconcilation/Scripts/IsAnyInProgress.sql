if (exists
	(select
		1
		from dbo.BalanceReconcilation
		where FirmId = @firmId
			and SettlementAccountId = @settlementAccountId
			and Status = @inProgressStatus))
	select 1;
else select 0;