update Accounting_PaymentOrder
	set PaymentSnapshot.modify('replace value of (PaymentOrder/Payer/Kpp/text())[1] with sql:variable("@Kpp")') 
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId
