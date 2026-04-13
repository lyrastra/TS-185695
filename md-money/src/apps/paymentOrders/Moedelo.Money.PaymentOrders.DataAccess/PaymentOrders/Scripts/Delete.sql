set nocount on;

delete from dbo.Accounting_PaymentOrder
	where FirmId = @firmId
		and DocumentBaseId = @documentBaseId

