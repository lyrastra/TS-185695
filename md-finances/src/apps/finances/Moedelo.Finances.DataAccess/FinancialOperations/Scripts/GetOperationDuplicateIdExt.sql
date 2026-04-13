set nocount on;
set transaction isolation level read uncommitted;

declare @id int = null

select @id = fo.id 
from dbo.FinancialOperation as fo
    inner join dbo.MoneyTransferOperation as mto on mto.id = fo.id
    inner join dbo.MoneyTransferOperationType as mtot on mtot.name = fo.type
where fo.firm_id = @FirmId
      and (@PaymentOrderNumber is null or isnull(mto.number_of_document, '') = iif(@Direction = 1, isnull(mto.number_of_document, ''), isnull(@PaymentOrderNumber, '')))	
	  and mto.summ = @Sum
	  and mtot.type = @Direction
      and fo.operation_date between @StartDate and @EndDate
	  and mto.destanition_description = @DestinationDescription
      and (@SettlementAccountId is null or mto.SettlementAccountId = @SettlementAccountId)
if @id is null
begin
	select @id = fo.id 
	from dbo.FinancialOperation as fo
		inner join dbo.MoneyTransferOperation as mto on mto.id = fo.id
		inner join dbo.MoneyTransferOperationType as mtot on mtot.name = fo.type
	where fo.firm_id = @FirmId
		  and (@PaymentOrderNumber is null or isnull(mto.number_of_document, '') = iif(@Direction = 1, isnull(mto.number_of_document, ''), isnull(@PaymentOrderNumber, '')))	
		  and mto.summ = @Sum
		  and mtot.type = @Direction
		  and fo.operation_date between @StartDate and @EndDate
		  and (@SettlementAccountId is null or mto.SettlementAccountId = @SettlementAccountId)
	order by fo.id desc;

	select @id as Id, 0 as IsStrict
end
else
begin
	select @id as Id, 1 as IsStrict
end
