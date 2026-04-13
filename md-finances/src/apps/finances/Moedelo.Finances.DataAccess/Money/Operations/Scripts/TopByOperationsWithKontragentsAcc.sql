select top (@count)
	d.KontragentId,
	sum(d.totalSum) as TotalSum
from (select KontragentId,
				sum(po.Sum) as totalSum
			from dbo.Accounting_PaymentOrder as po
			where FirmId = @firmId
			and po.Date between @startDate and @endDate
			and po.KontragentId is not null
			group by KontragentId
		union all
		select KontragentId,
				sum(co.Sum) as totalSum
			from dbo.Accounting_CashOrder as co
			where FirmId = @firmId
			and co.Date between @startDate and @endDate
			and co.KontragentId is not null
			group by KontragentId
		union all
		select KontragentId,
				sum(purop.Sum) as totalSum
			from docs.PurseOperation as purop
			where FirmId = @firmId
			and purop.DocumentDate between @startDate and @endDate
			and purop.KontragentId is not null
			group by KontragentId) as d
group by d.KontragentId
order by TotalSum desc