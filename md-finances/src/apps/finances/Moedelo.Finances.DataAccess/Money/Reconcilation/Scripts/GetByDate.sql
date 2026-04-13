select Id,
       SettlementAccountId,
       ServiceBalance,
       BankBalance,
       ReconcilationDate,
       CreateDate,
       SessionId,
       Status
from dbo.BalanceReconcilation br
where br.FirmId = @FirmId
  and br.ReconcilationDate = @Date
  and br.SettlementAccountId in @SettlementAccountIds;
