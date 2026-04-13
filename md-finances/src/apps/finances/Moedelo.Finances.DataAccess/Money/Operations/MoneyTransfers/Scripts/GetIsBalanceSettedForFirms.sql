select
	f.FirmId													as [Key],
	convert(bit, iif(max(fo.operation_date) is not null, 1, 0))	as [Value]
	from #FirmIds as f
		left outer join dbo.FinancialOperation as fo on
			f.FirmId = fo.firm_id
				and fo.[type] = 'CurrencyBalanceOperation'
		left outer join dbo.MoneyTransferOperation as mto on
			fo.id = mto.id
				and mto.money_bay_type = 0
	group by f.FirmId;