set nocount on;

merge dbo.RentalPayment as tgt
using #Periods as src
on tgt.RentalPaymentItemId = src.RentalPaymentItemId
and tgt.PaymentBaseId = @DocumentBaseId
and tgt.FirmId = @FirmId
when matched then update set
					tgt.PaymentSum = src.PaymentSum
when not matched then insert
						(FirmId,
						PaymentBaseId,
						RentalPaymentItemId,
						PaymentSum)			
					values
						(@FirmId,
						@DocumentBaseId,
						src.RentalPaymentItemId,
						src.PaymentSum)			
when not matched by source and tgt.PaymentBaseId = @DocumentBaseId then delete;