select
	Id,
	FirmId,
	DocumentBaseId,
	WorkerId,
	PaymentSum,
	TakeInTaxationSystem as TaxationSystem
	from dbo.WorkerPayment
	where FirmId = @FirmId
		--DocumentBaseIdFilter-- and DocumentBaseId = @DocumentBaseId
		--DocumentBaseIdsFilter-- and DocumentBaseId in @DocumentBaseIds;