select PaymentSnapshot.value('(/PaymentOrder/Payer/Kpp/node())[1]', 'varchar(9)') as Kpp
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId
