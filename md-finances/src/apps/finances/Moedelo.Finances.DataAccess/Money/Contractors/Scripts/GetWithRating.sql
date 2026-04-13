select q.KontragentId as Id,
		q.KontragentType as Type,
		count(1) as Rating
	from (select coalesce(po.KontragentId, po.SalaryWorkerId, wpi.WorkerId) as KontragentId,
					iif(po.KontragentId is null, @WorkerType, @KontragentType) as KontragentType
				from dbo.Accounting_PaymentOrder po
					left join dbo.WorkerPayment as wpi
						on wpi.DocumentBaseId = po.DocumentBaseId
				where po.FirmId = @FirmId
					and (po.KontragentId is not null
					or po.SalaryWorkerId is not null
					or wpi.WorkerId is not null)
			union all
			select coalesce(co.KontragentId, co.SalaryWorkerId, wpi.WorkerId) as KontragentId,
					case	when co.KontragentId is not null then @KontragentType
							when isnull(co.SalaryWorkerId, wpi.WorkerId) is not null then @WorkerType
					end as KontragentType
				from dbo.Accounting_CashOrder as co
					left join dbo.CashOrderWorkerPayItem as wpi
						on wpi.CashOrderId = co.Id
				where co.FirmId = @FirmId
					and (KontragentId is not null
					or co.SalaryWorkerId is not null
					or wpi.WorkerId is not null)
			union all
			select KontragentId,
					@KontragentType as KontragentType
				from docs.PurseOperation
				where FirmId = @FirmId
					and KontragentId is not null) as q
	group by q.KontragentId,
		q.KontragentType;
