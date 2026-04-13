with rp_paid (RentalPaymentItemId, PaidSum) as
	(select
		rp_paid.RentalPaymentItemId,
		sum(rp_paid.PaymentSum) as PaidSum
		from dbo.RentalPayment as rp_paid
		where rp_paid.FirmId = @FirmId
		group by rp_paid.RentalPaymentItemId)
	select
		rp.Id,
		rp.FirmId,
		rp.PaymentBaseId,
		rp.RentalPaymentItemId,
		rp.PaymentSum,
		(rpi.PaymentSum + rpi.BuyoutSum - rpi.AdvanceSum - rp_paid.PaidSum) as PaymentRequiredSum
		from dbo.RentalPayment				as rp
			left join dbo.RentalPaymentItem as rpi on
				rp.RentalPaymentItemId = rpi.Id
			left join rp_paid on
				rp.RentalPaymentItemId = rp_paid.RentalPaymentItemId
		where PaymentBaseId = @DocumentBaseId
			and rp.FirmId = @FirmId;
