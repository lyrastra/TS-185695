set nocount on;
update dbo.UnifiedTaxPayment
	set TaxPostingType = @TaxPostingType
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId;