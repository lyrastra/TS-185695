select top 1
	DocumentBaseId
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and Id = @Id;