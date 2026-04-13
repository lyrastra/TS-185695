if exists (select 1
			from dbo.FinancialOperation as fo
				join dbo.MoneyTransferOperation as mto
					on mto.id = fo.id
			where fo.firm_id = @FirmId
				and fo.type <> 'CurrencyBalanceOperation'
				and mto.SettlementAccountId = @SettlementAccountId)
	select 1;
else
	select 0;
