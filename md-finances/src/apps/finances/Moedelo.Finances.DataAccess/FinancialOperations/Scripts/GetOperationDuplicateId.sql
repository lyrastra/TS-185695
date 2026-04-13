select fo.id
	from dbo.FinancialOperation as fo
		inner join dbo.MoneyTransferOperation as mto
			on mto.id = fo.id
		inner join dbo.MoneyTransferOperationType as mtot
			on fo.type = mtot.name
		left outer join dbo.Kontragent as k
			on mto.kontragent_id = k.id
		left outer join dbo.KontragentSettlementAccount as ksa
			on mto.KontragentSettlementAccountId = ksa.Id
		left outer join dbo.Salary_WorkerCardAccount as swca
			on mto.worker_id = swca.worker_id
	where fo.firm_id = @FirmId
		and mtot.type = @Direction
		and fo.type != @CurrencyBalanceOperationType
		and fo.operation_date between @StartDate and @EndDate
		and mto.summ = @Sum
		and isnull(mto.number_of_document, '') = isnull(@PaymentOrderNumber, '')
		and (@DestinationDescription is null or substring(mto.destanition_description, 1, 210) = substring(@DestinationDescription, 1, 210))
		and (mto.SettlementAccountId = @SettlementAccountId
		or (fo.type = @CurrencyPurchaseAndSaleOutgoingOperationType and mto.MovementSettlementAccountId = @SettlementAccountId))
		and (isnull(@ContractorInn, '') = ''
		or (isnull(mto.budgetary_payment_type, 0) <> @BudgetaryPaymentOtherType and (mto.kontragent_id is null or isnull(k.inn, '') = '' or (k.inn = @ContractorInn and (isnull(@ContractorSettlementAccount, '') = '' or isnull(ksa.Number, '') = '' or ksa.Number = @ContractorSettlementAccount))))
		or (mto.budgetary_payment_type = @BudgetaryPaymentOtherType and (isnull(@ContractorSettlementAccount, '') = '' or isnull(mto.RecipientSettlement, '') = '' or mto.RecipientInn = @ContractorInn)))
	order by case	when fo.operation_date = @Date
							and (ksa.Number = @ContractorSettlementAccount
							or mto.RecipientSettlement = @ContractorSettlementAccount
							or swca.number = @ContractorSettlementAccount) then 1
					when (ksa.Number = @ContractorSettlementAccount
							or mto.RecipientSettlement = @ContractorSettlementAccount
							or swca.number = @ContractorSettlementAccount) then 2
					when fo.operation_date = @Date then 3
				end,
			fo.id desc;
