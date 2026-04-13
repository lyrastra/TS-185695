declare @payed tinyint = 6;

select
	r.PaymentBaseId,
	p.Date as PaymentDate, 
	r.RentalPaymentItemId,
	r.PaymentSum
	from dbo.RentalPayment						as r
		inner join dbo.Accounting_PaymentOrder	as p on
			r.PaymentBaseId = p.DocumentBaseId
	where r.FirmId = @firmId
		and p.PaidStatus = @payed
		and r.PaymentBaseId in (select DocumentBaseId from #Payments);
