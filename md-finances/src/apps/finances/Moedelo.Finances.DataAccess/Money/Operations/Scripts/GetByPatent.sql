select po.DocumentBaseId,
		po.Date,
		po.PaymentNumber as Number,
		po.Direction,
		po.KontragentName,
		po.OperationType,
		po.OperationState,
		@PaymentOrderOperationKind as OperationKind,
		po.PaidStatus,
		po.Sum,
		po.TotalSum,
		sa.id_currency as Currency,
		po.Description,
		bpo.DocumentBaseId as BaseOperationId,
		po.SettlementAccountId as SettlementAccountId,
		po.IsIgnoreNumber,
        po.TaxationSystemType
	from dbo.Accounting_PaymentOrder as po
		join dbo.SettlementAccount as sa
			on sa.id = po.SettlementAccountId
		left join dbo.Accounting_PaymentOrder as bpo
			on bpo.id = po.DuplicateId
	where po.FirmId = @FirmId
		and po.PatentId = @PatentId
union all
select co.DocumentBaseId,
		co.Date,
		co.Number,
		co.Direction,
		null as KontragentName,
		co.OperationType,
		@RegularOperationState as OperationState,
		@CashOrderOperationKind as OperationKind,
		@PayedDocumentStatus as PaidStatus,
		co.Sum,
		null as TotalSum,
		@CurrencyRub as Currency,
		co.Destination as Description,
		null as BaseOperationId,
		co.SetlementAccountId as SettlementAccountId,
		null,
        co.TaxationSystemType
	from dbo.Accounting_CashOrder as co
	where co.FirmId = @FirmId
		and co.PatentId = @PatentId