if exists (select 1
			from dbo.Accounting_PaymentOrder
			where FirmId = @FirmId
				and SettlementAccountId = @SettlementAccountId)
	select 1;
else
	select 0;
