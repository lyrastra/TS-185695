select max(dates.Date)
	from (select max(Date) as Date
				from dbo.Accounting_PaymentOrder
				where FirmId = @FirmId
					and Date <= @Date
			union all
			select max(Date) as Date
				from dbo.Accounting_CashOrder
				where FirmId = @FirmId
					and Date <= @Date
			union all
			select max(DocumentDate) as Date
				from docs.PurseOperation
				where FirmId = @FirmId
					and DocumentDate <= @Date) as dates;
