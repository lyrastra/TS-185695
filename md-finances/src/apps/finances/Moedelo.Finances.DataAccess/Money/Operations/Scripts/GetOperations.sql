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
		--FilterByBaseId--and po.DocumentBaseId = @DocumentBaseId
		--FilterByBaseIds--and po.DocumentBaseId in (select Id from @BaseIds)
		--FilterByPeriod--and po.Date between @StartDate and @EndDate
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
		--FilterByBaseId--and co.DocumentBaseId = @DocumentBaseId
		--FilterByBaseIds--and co.DocumentBaseId in (select Id from @BaseIds)
		--FilterByPeriod--and co.Date between @StartDate and @EndDate
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
	  null as TotalSum,
		@CurrencyRub as Currency,
		po.Comment as Description,
		null as BaseOperationId,
		po.SettlementAccountId as SettlementAccountId,
		null,
        null as TaxationSystemType
	from docs.PurseOperation as po
	where po.FirmId = @FirmId
		--FilterByBaseId--and po.DocumentBaseId = @DocumentBaseId
		--FilterByBaseIds--and po.DocumentBaseId in (select Id from @BaseIds)
		--FilterByPeriod--and po.DocumentDate between @StartDate and @EndDate