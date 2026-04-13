/*
declare @FirmId int = 4359699;
declare @Status int = 3;
*/

;with OrderedSettlementAccountIds as (
	select 
		br.Id,
		br.SettlementAccountId,
		br.ServiceBalance,
		br.BankBalance,
		br.ReconcilationDate,
		br.CreateDate,
		br.SessionId,
		br.Status,
	    row_number() over(partition by br.SettlementAccountId order by br.Id desc) as RowNumber
	from BalanceReconcilation br
		join @SettlementAccountIds sa on sa.Id = br.SettlementAccountId
	where br.FirmId = @FirmId 
	and br.Status = @Status
)
select 
	Id,
	SettlementAccountId,
	ServiceBalance,
	BankBalance,
	ReconcilationDate,
	CreateDate,
	SessionId,
	Status
from OrderedSettlementAccountIds o
where o.RowNumber = 1;