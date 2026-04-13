set nocount on;

delete from dbo.RentalPayment
	where PaymentBaseId = @documentBaseId
		and FirmId = @firmId;
