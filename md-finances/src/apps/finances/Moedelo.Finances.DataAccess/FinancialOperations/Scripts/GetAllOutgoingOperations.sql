select
	fo.id,
	fo.DocumentBaseId,
	mtot.type																								as Direction,
	mto.destanition_description																				as Description,
	mto.SettlementAccountId,
	mto.kontragent_id																						as KontragentId,
	iif(mto.number_of_document = '', fo.number, mto.number_of_document)										as Number,
	mto.settlement_account																					as PayerSettlementNumber,
	''																										as PayerInn,
	coalesce(ksa.Number, mto.RecipientSettlement)															as RecipientSettlementNumber,
	coalesce(k.inn, mto.RecipientInn)																		as RecipientInn,
	iif(fo.type = @PayDaysOutgoingOperationType and mto.money_bay_type = @SalaryProjectMoneyBayType, 1, 0)	as IsSalaryOperation
	from dbo.FinancialOperation						as fo
		inner join dbo.MoneyTransferOperation		as mto on
			mto.id = fo.id
		inner join dbo.MoneyTransferOperationType	as mtot on
			fo.type = mtot.name
		left join dbo.Kontragent					as k on
			k.id = mto.kontragent_id
		left join dbo.KontragentSettlementAccount	as ksa on
			ksa.Id = mto.KontragentSettlementAccountId
	where fo.firm_id = @FirmId
		and
			(mto.SettlementAccountId = @SettlementAccountId
			and
				(mtot.type = @Direction
				or mtot.name = 'MovementFromSettlementToCashMoneyTransferOperation')
			or mto.MovementSettlementAccountId = @SettlementAccountId
			and mtot.name in ('MovementFromSettlementToSettlementMoneyTransferOperation',
							'CurrencyPurchaseAndSaleOutgoingOperation'))
		and mto.summ = @Sum
		and fo.operation_date between @StartDate and @EndDate;