select	po.DocumentBaseId,
		po.PaidStatus as DocumentStatus
	from dbo.Accounting_PaymentOrder as po
		join @BaseIds as ids
			on ids.Id = po.DocumentBaseId
	where po.FirmId = @FirmId;
