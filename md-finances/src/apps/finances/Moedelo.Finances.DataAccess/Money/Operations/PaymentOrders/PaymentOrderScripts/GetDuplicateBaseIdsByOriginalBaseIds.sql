select po2.DocumentBaseId
	from dbo.Accounting_PaymentOrder as po
		join @BaseIds as baseIds
			on baseIds.Id = po.DocumentBaseId
		join dbo.Accounting_PaymentOrder as po2
			on po2.DuplicateId = po.Id
	where po.FirmId = @FirmId and po2.FirmId = @FirmId;