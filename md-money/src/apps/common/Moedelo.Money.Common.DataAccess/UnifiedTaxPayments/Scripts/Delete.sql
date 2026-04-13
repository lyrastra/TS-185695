set nocount on;

delete from dbo.UnifiedTaxPayment
	output deleted.DocumentBaseId
	where ParentDocumentId = @ParentBaseId
		and FirmId = @firmId;
