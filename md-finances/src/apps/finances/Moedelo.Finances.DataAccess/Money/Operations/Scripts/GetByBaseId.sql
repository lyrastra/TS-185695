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
		sa.id_currency as Currency,
		po.Description,
		bpo.DocumentBaseId as BaseOperationId
	from dbo.Accounting_PaymentOrder as po
		join dbo.SettlementAccount as sa
			on sa.id = po.SettlementAccountId
		left join dbo.Accounting_PaymentOrder as bpo
			on bpo.id = po.DuplicateId
	where po.FirmId = @FirmId
		and po.DocumentBaseId = @DocumentBaseId
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
		@CurrencyRub as Currency,
		co.Destination as Description,
		null as BaseOperationId
	from dbo.Accounting_CashOrder as co
	where co.FirmId = @FirmId
		and co.DocumentBaseId = @DocumentBaseId
union all
select po.DocumentBaseId,
		po.DocumentDate as Date,
		po.DocumentNumber as Number,
		po.Direction,
		null as KontragentName,
		po.OperationType,
		@RegularOperationState as OperationState,
		@PurseOperationOperationKind as OperationKind,
		@PayedDocumentStatus as PaidStatus,
		po.Sum,
		@CurrencyRub as Currency,
		po.Comment as Description,
		null as BaseOperationId
	from docs.PurseOperation as po
	where po.FirmId = @FirmId
		and po.DocumentBaseId = @DocumentBaseId;
