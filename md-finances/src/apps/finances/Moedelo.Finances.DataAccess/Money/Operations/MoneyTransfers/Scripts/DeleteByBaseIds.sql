declare @ids dbo.IntList;
insert into @ids(Id)
	select id from dbo.FinancialOperation
		where firm_id = @FirmId
			and DocumentBaseId in (select Id from @BaseIds);
delete from dbo.MoneyTransferOperation
	where id in (select Id from @ids);
delete from dbo.FinancialOperation
	where id in (select Id from @ids);

