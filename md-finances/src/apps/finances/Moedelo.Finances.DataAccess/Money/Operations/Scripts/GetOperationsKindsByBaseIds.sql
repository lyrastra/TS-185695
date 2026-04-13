select @PaymentOrderOperationKind as OperationKind,
		DocumentBaseId
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and DocumentBaseId in (select Id from @BaseIds)
union all
select @CashOrderOperationKind as OperationKind,
		DocumentBaseId
	from dbo.Accounting_CashOrder
	where FirmId = @FirmId
		and DocumentBaseId in (select Id from @BaseIds)
union all
select @PurseOperationOperationKind as OperationKind,
		DocumentBaseId
	from docs.PurseOperation
	where FirmId = @FirmId
		and DocumentBaseId in (select Id from @BaseIds);
