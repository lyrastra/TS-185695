select top 1
	end_date
	from dbo.CashClosedGroup
	where firm_id = @Firmid
		and end_date is not null
	order by end_date desc;