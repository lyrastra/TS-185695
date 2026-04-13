select fo.id
	from dbo.FinancialOperation as fo
		inner join dbo.MoneyTransferOperation as mto
			on mto.id = fo.id
		inner join dbo.MoneyTransferOperationType as mtot
			on fo.type = mtot.name
	where fo.firm_id = @FirmId
		and fo.type = 'MaterialAidOperation'
		and fo.operation_date between @StartDate and @EndDate
		and mto.summ = @Sum
		and mto.SettlementAccountId = @SettlementAccountId
		and mto.kontragent_id = @KontragentId
	order by fo.id desc;
