select
	fo.id,
	fo.DocumentBaseId,
	case 
		when mtot.type = @IncomingDirection
				or mtot.name in ('MovementFromCashToSettlementMoneyTransferOperation',
								'MovementFromPurseToSettlementMoneyTransferOperation',
								'MovementFromSettlementToSettlementMoneyTransferOperation') then 1
		when (mtot.type = @OutgoingDirection
				or mtot.name = 'MovementFromSettlementToCashMoneyTransferOperation')
			or mto.MovementSettlementAccountId = @SettlementAccountId
			and mtot.name in ('MovementFromSettlementToSettlementMoneyTransferOperation',
							'CurrencyPurchaseAndSaleOutgoingOperation') then 2
	end																										as Direction,
	fo.operation_date																						as Date,
	mto.summ																								as Sum,
	mto.destanition_description																				as Description,
	mto.number_of_document																					as Number,
	case
		when (mtot.type = @OutgoingDirection
			or mtot.name = 'MovementFromSettlementToCashMoneyTransferOperation')
			or mto.MovementSettlementAccountId = @SettlementAccountId
			and mtot.name in ('MovementFromSettlementToSettlementMoneyTransferOperation',
							'CurrencyPurchaseAndSaleOutgoingOperation') then
			coalesce(ksa.Number, mto.RecipientSettlement)
		when mtot.type = @IncomingDirection
			or mtot.name in ('MovementFromCashToSettlementMoneyTransferOperation',
							'MovementFromPurseToSettlementMoneyTransferOperation',
							'MovementFromSettlementToSettlementMoneyTransferOperation') then mto.settlement_account
		else ''
	end																										as ContractorSettlementAccount,
	case
		when (mtot.type = @OutgoingDirection
			or mtot.name = 'MovementFromSettlementToCashMoneyTransferOperation')
			or mto.MovementSettlementAccountId = @SettlementAccountId
			and mtot.name in ('MovementFromSettlementToSettlementMoneyTransferOperation',
							'CurrencyPurchaseAndSaleOutgoingOperation') then coalesce(k.inn, mto.RecipientInn)
		else ''
	end																										as ContractorInn,
	iif(fo.type = @PayDaysOutgoingOperationType and mto.money_bay_type = @SalaryProjectMoneyBayType, 1, 0)	as IsSalaryOperation,
	0																										as IsProfitWithdrawingOperation
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
				(mtot.type = @IncomingDirection
				or mtot.name in ('MovementFromCashToSettlementMoneyTransferOperation',
								'MovementFromPurseToSettlementMoneyTransferOperation',
								'MovementFromSettlementToSettlementMoneyTransferOperation')))
		and fo.operation_date between @StartDate and @EndDate;
