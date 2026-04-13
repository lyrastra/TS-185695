set nocount on;

delete from dbo.Accounting_CashOrder
	where FirmId = @firmId
		and DocumentBaseId = @documentBaseId

