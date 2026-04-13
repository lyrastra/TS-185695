set nocount on;

delete from dbo.WorkerPayment
	where DocumentBaseId = @documentBaseId
		and FirmId = @firmId;
