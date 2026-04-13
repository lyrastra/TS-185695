with operations
as (select po.DocumentBaseId,
           po.Date,
           po.PaymentNumber as Number,
           po.Direction,
           po.KontragentName,
           po.OperationType,
           po.OperationState,
           po.Sum,
           sa.id_currency as Currency,
           po.Description,
           bpo.DocumentBaseId as BaseOperationId
    from dbo.Accounting_PaymentOrder as po
        join dbo.SettlementAccount as sa
            on sa.id = po.SettlementAccountId
        left join dbo.Accounting_PaymentOrder as bpo
            on bpo.id = po.DuplicateId
    where po.FirmId = @FirmId
          and isnull(po.OperationState, @RegularOperationState) in (select Id from @UnrecognizedOperationStates)
          and isnull(po.OutsourceState, 0) <> @UnconfirmedOutsourceState
          and po.Date >= @InitialDate
          and (@SourceId is null or po.SettlementAccountId = @SourceId)),
     summary
as (select count(1) as TotalCount
    from operations)
select 1 as isSummary,
       0 as DocumentBaseId,
       null as Date,
	   '' as Number,
       0 as Direction,
       null as KontragentName,
       0 as OperationType,
       0 as OperationState,
       0 as Sum,
       0 as Currency,
       null as Description,
       null as BaseOperationId,
       summary.TotalCount
from summary
union all
select 0,
       DocumentBaseId,
       Date,
	   Number,
       Direction,
       KontragentName,
       OperationType,
       OperationState,
       Sum,
       Currency,
       Description,
       BaseOperationId,
       0
from
(
    select operations.DocumentBaseId,
           operations.Date,
		   operations.Number,
           operations.Direction,
           operations.KontragentName,
           operations.OperationType,
           operations.OperationState,
           operations.Sum,
           operations.Currency,
           operations.Description,
           operations.BaseOperationId
    from operations
    order by operations.Date offset @Offset rows fetch next @Count rows only
) as ops;
