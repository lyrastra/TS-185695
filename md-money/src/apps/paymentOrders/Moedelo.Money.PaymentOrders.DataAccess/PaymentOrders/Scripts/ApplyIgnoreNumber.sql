set nocount on;

update dbo.Accounting_PaymentOrder set
	IsIgnoreNumber = 1
where FirmId = @firmId
	and DocumentBaseId in (select Id from #BaseIds);