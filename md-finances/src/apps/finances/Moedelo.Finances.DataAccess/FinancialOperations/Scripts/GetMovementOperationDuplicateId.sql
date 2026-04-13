select fo.id
	from dbo.FinancialOperation as fo
		inner join dbo.MoneyTransferOperation as mto
			on mto.id = fo.id
		inner join dbo.MoneyTransferOperationType as mtot
			on fo.type = mtot.name
	where fo.firm_id = @FirmId
		and mtot.type = @Direction
		and fo.type != @CurrencyBalanceOperationType
		and fo.operation_date between @StartDate and @EndDate
		and mto.summ = @Sum
		and isnull(mto.number_of_document, '') = isnull(@PaymentOrderNumber, '')
		and (mto.SettlementAccountId = @MovementSettlementAccountId
		and (@SettlementAccountId = null or mto.MovementSettlementAccountId = @SettlementAccountId or mto.MovementSettlementAccountId is null)
		or mto.SettlementAccountId = @SettlementAccountId and mto.MovementSettlementAccountId = @MovementSettlementAccountId)
	order by fo.id desc;
