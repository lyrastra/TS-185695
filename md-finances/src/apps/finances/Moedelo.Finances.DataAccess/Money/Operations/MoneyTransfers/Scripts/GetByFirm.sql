select /* TopConstraint */
	fo.Id,
	fo.DocumentBaseId,
	fo.operation_date as Date,
	fo.number as Number,
	case
		when mtot.type = 1 then 2
		when mtot.type = 2 then 1
		when mtot.type = 3
			and fo.type = 'MovementFromSettlementToCashMoneyTransferOperation' then 1
		when mtot.type = 3
			and fo.type = 'MovementFromCashToSettlementMoneyTransferOperation' then 2
		when mtot.type = 3
			and fo.type = 'MovementFromPurseToSettlementMoneyTransferOperation' then 2
		when mtot.type = 3
			and t.f = 1
			and fo.type = 'MovementFromSettlementToSettlementMoneyTransferOperation' then 2
		when mtot.type = 3
			and t.f = 2
			and fo.type = 'MovementFromSettlementToSettlementMoneyTransferOperation' then 1
	end as Direction,
	iif(isnull(t.f, 1) = 1, mto.SettlementAccountId, mto.MovementSettlementAccountId) as SettlementAccountId,
	iif(t.f = 2, mto.SettlementAccountId, mto.MovementSettlementAccountId) as MovementSettlementAccountId,
	mto.kontragent_id as KontragentId,
	'' as KontragentName,
	mto.KontragentSettlementAccountId,
	mto.worker_id as WorkerId,
	fo.type as OperationType,
	mto.summ as Sum,
	mto.destanition_description as Description,
	mto.money_bay_type as MoneyBayType,
	mto.purse_id as PurseId,
	-- PayDays
	mto.PaymentType,
	mto.bank_settlement_account as BankSettlementAccount,
	-- LoansThirdParties
	mto.recipient,
	-- BudgetaryPayment
	mto.budgetary_payment_type as BudgetaryPaymentType,
	mto.BudgetaryPaymentSubtype,
	mto.BudgetaryPaymentFoundation,
	mto.kbk,
	mto.Okato,
	mto.Oktmo,
	mto.code_uin as CodeUin,
	mto.envd_taxadministration_id as EnvdTaxAdministrationId
	from dbo.FinancialOperation as fo
		join dbo.MoneyTransferOperation as mto
			on mto.id = fo.id
		join dbo.MoneyTransferOperationType as mtot
			on mtot.name = fo.type
		left join (select t.f from (values (1), (2)) as t (f) ) as t
			on mtot.type = 3
			and fo.type = 'MovementFromSettlementToSettlementMoneyTransferOperation'
	where fo.firm_id = @Firmid
