select fo.id as Id
	from dbo.FinancialOperation as fo
		inner join dbo.MoneyTransferOperation as mto
			on mto.id = fo.id
		inner join dbo.MoneyTransferOperationType as mtot
			on fo.type = mtot.name
	where fo.firm_id = @FirmId
		and mtot.type = @Direction
		and fo.type != @CurrencyBalanceOperationType
		and fo.operation_date = convert(date, @Date)
		and mto.summ = @Sum
		and mto.number_of_document = @PaymentOrderNumber
	order by fo.id;
