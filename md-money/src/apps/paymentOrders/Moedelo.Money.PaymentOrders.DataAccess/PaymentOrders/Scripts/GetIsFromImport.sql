select
	iif(SourceFileId is null, 0, 1) as IsFromImport
from dbo.Accounting_PaymentOrder
where FirmId = @firmId
	and DocumentBaseId = @documentBaseId;
