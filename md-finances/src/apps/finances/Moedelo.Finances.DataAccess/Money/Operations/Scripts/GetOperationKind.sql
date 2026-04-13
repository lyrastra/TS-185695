select 1
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId
union all
select 2
	from dbo.Accounting_CashOrder
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId
union all
select 3
	from docs.PurseOperation
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId;