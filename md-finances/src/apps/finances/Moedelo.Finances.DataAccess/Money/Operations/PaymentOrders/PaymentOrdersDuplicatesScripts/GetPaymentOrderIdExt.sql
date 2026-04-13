set nocount on;
set transaction isolation level read uncommitted;

declare @id int = null
declare @baseId bigint = null

select @id = po.Id, @baseId = po.DocumentBaseId
	from dbo.Accounting_PaymentOrder as po
	where po.FirmId = @FirmId
		and po.PaymentNumber = iif(@Direction = 1, po.PaymentNumber, @PaymentOrderNumber)
		and po.Sum = @Sum
		and po.Direction = @Direction
		and po.Date = @Date
		and (@SettlementAccountId is null or po.SettlementAccountId = @SettlementAccountId)
		and po.Description = @DestinationDescription
		and isnull(po.OperationState, @RegularOperationState) not in (select Id from @BadOperationStates);

if @id is null
begin
	select @id = po.Id, @baseId = po.DocumentBaseId
		from dbo.Accounting_PaymentOrder as po
		where po.FirmId = @FirmId
			and po.PaymentNumber = iif(@Direction = 1, po.PaymentNumber, @PaymentOrderNumber)
			and po.Sum = @Sum
			and po.Direction = @Direction
			and po.Date between @StartDate and @EndDate
			and (@SettlementAccountId is null or po.SettlementAccountId = @SettlementAccountId)
			and isnull(po.OperationState, @RegularOperationState) not in (select Id from @BadOperationStates)
		order by iif(po.Date = @Date, 0, 1),
			po.Id desc;

	select @id as Id, @baseId as BaseId, 0 as IsStrict
end
else
begin
	select @id as Id, @baseId as BaseId, 1 as IsStrict
end