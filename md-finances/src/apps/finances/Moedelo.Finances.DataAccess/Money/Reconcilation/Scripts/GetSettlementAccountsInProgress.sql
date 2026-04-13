select distinct(SettlementAccountId)
from dbo.BalanceReconcilation
where FirmId = @FirmId
	and Status = @InProgressStatus
	and CreateDate >= @ActualDays
	and SettlementAccountId in (select Id from @SettlementAccountIds);