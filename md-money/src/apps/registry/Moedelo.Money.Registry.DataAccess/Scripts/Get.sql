with WorkerPaymentAgg as (
	select 
		DocumentBaseId,
		iif(count(WorkerId) = 1, min(WorkerId), null) as WorkerId
	from dbo.WorkerPayment
	where FirmId = @firmId
	group by DocumentBaseId
),
CashOrderWorkerAgg as (
	select
		wpi.CashOrderId,
		iif(count(wpi.WorkerId) = 1, min(wpi.WorkerId), null) as WorkerId
	from dbo.CashOrderWorkerPayItem wpi
		join dbo.Accounting_CashOrder co on co.Id = wpi.CashOrderId
	where co.FirmId = @firmId
	group by wpi.CashOrderId
),
MainData as (
	select
		op.DocumentBaseId,
		op.PaymentNumber			as Number,
		op.Date,
		op.Sum,
		coalesce(op.KontragentId, op.SalaryWorkerId, wp.WorkerId) as ContractorId,
		case
			when op.KontragentId is not null then @KontragentTypeKontragent
			when coalesce(op.SalaryWorkerId, wp.WorkerId) is not null then @KontragentTypeWorker
			else @KontragentTypeAll
		end as ContractorType,
		op.KontragentName as ContractorName,
		op.Direction,
		op.SettlementAccountId as SourceId,
		@SourceSettlementAccount as SourceType,
		sa.Number as SourceName,
		op.OperationType,
		op.Description,
		op.ModifyDate,
		op.KontragentSettlementAccountId,
		iif(op.Direction = @DirectionOutgoing, iif(op.PaidStatus = @PayedStatus, 1, 0), 1) AS IsPaid,
		op.TaxationSystemType,
		op.PatentId,
		op.IncludeNds,
		op.NdsSum,
		op.NdsType,
		op.KontragentAccountCode
	from dbo.Accounting_PaymentOrder as op
		join dbo.SettlementAccount as sa on sa.id = op.SettlementAccountId
		left join WorkerPaymentAgg as wp on wp.DocumentBaseId = op.DocumentBaseId
	where op.FirmId = @firmId
		and coalesce(op.OperationState, @OperationStateDefault) not in @BadStates
		--PeriodFilter-- and op.Date between @StartDate and @EndDate
		--AfterDate-- and op.ModifyDate > @AfterDate
		--TaxationSystemType-- and op.TaxationSystemType = @TaxationSystemType
		--SourceFilter-- and @Source=@SourceSettlementAccount
		--OperationTypes-- and op.OperationType in (select Id from #OperationTypes)
		--ContractorFilter-- and coalesce(op.KontragentId, op.SalaryWorkerId, wp.WorkerId) = @ContractorId 
		--ContractorFilter-- and (case when op.KontragentId is not null then @KontragentTypeKontragent when coalesce(op.SalaryWorkerId, wp.WorkerId) is not null then @KontragentTypeWorker else @KontragentTypeAll end) = @ContractorType
		--QueryFilter-- and op.PaymentNumber like '%'+@Query+'%'
		--DocumentBaseIdsFilter-- and op.DocumentBaseId in (select Id from #DocumentBaseIds)
	
	union all
	
	select
		op.DocumentBaseId,
		op.Number,
		op.Date,
		op.Sum,
		coalesce(op.KontragentId, op.SalaryWorkerId, wpi.WorkerId) as ContractorId,
		case
			when op.KontragentId is not null then @KontragentTypeKontragent
			when coalesce(op.SalaryWorkerId, wpi.WorkerId) is not null then @KontragentTypeWorker
			else @KontragentTypeAll
		end as ContractorType,
		case when wpi.WorkerId is not null and op.PaybillNumber is not null 
			then 'ведомость №' + op.PaybillNumber + ' от ' + convert(varchar, op.PaybillDate, 104)
			else op.DestinationName
		end as ContractorName,
		op.Direction,
		op.CashId as SourceId,
		@SourceCashbox as SourceType,
		fc.Name as SourceName,
		op.OperationType,
		op.Destination as Description,
		op.ModifyDate,
		1 as KontragentSettlementAccountId,
		1 as IsPaid,
		op.TaxationSystemType,
		op.PatentId as PatentId,
		op.IncludeNds,
		op.NdsSum,
		op.NdsType,
		sat.Code as KontragentAccountCode
	from dbo.Accounting_CashOrder op
		join dbo.FirmCash fc on fc.Id = op.CashId
		left join CashOrderWorkerAgg as wpi on wpi.CashOrderId = op.Id
		left join dbo.SyntheticAccountType as sat on op.SyntheticAccountTypeId = sat.id
	where op.FirmId = @firmId
		--PeriodFilter-- and op.Date between @StartDate and @EndDate
		--AfterDate-- and op.ModifyDate > @AfterDate
		--TaxationSystemType-- and op.TaxationSystemType = @TaxationSystemType
		--SourceFilter-- and @Source=@SourceCashbox
		--OperationTypes-- and op.OperationType in (select Id from #OperationTypes)
		--ContractorFilter-- and coalesce(op.KontragentId, op.SalaryWorkerId, wpi.WorkerId) = @ContractorId 
		--ContractorFilter-- and (case when op.KontragentId is not null then @KontragentTypeKontragent when coalesce(op.SalaryWorkerId, wpi.WorkerId) is not null then @KontragentTypeWorker else @KontragentTypeAll end) = @ContractorType
		--QueryFilter-- and op.Number like '%'+@Query+'%'
		--DocumentBaseIdsFilter-- and op.DocumentBaseId in (select Id from #DocumentBaseIds)
	
	union all
	
	select
		op.DocumentBaseId,
		op.DocumentNumber as Number,
		op.DocumentDate as Date,
		op.Sum,
		k.id as ContractorId,
		iif(op.KontragentId is not null, @KontragentTypeKontragent, @KontragentTypeAll) as ContractorType,
		coalesce(k.ShortName, k.Fio) as ContractorName,
		op.Direction,
		op.PurseId as SourceId,
		@SourcePurse as SourceType,
		p.Name as SourceName,
		op.OperationType,
		op.Comment as Description,
		op.ModifyDate,
		1 as KontragentSettlementAccountId,
		1 as IsPaid,
		op.TaxSystemType as TaxationSystemType,
		null as PatentId,
		0 as IncludeNds,
		null as NdsSum,
		null as NdsType,
		null as KontragentAccountCode
	from docs.PurseOperation as op
		left join dbo.Purse as p on p.id = op.PurseId
		left join dbo.Kontragent as k on k.id = op.KontragentId
	where op.FirmId = @firmId
		--PeriodFilter-- and op.DocumentDate between @StartDate and @EndDate
		--AfterDate-- and op.ModifyDate > @AfterDate
		--TaxationSystemType-- and op.TaxSystemType = @TaxationSystemType
		--SourceFilter-- and @Source=@SourcePurse
		--OperationTypes-- and op.OperationType in (select Id from #OperationTypes)
		--ContractorFilter-- and k.id = @ContractorId 
		--ContractorFilter-- and (iif(op.KontragentId is not null, @KontragentTypeKontragent, @KontragentTypeAll)) = @ContractorType
		--QueryFilter-- and op.DocumentNumber like '%'+@Query+'%'
		--DocumentBaseIdsFilter-- and op.DocumentBaseId in (select Id from #DocumentBaseIds)
),
TotalCountCTE as (
	select count(1) as TotalCount
	from MainData
)

select
	p.DocumentBaseId,
	p.Number,
	p.Date,
	p.Sum,
	p.ContractorId,
	p.ContractorType,
	p.ContractorName,
	p.Direction,
	p.SourceId,
	p.SourceType,
	p.SourceName,
	p.OperationType,
	p.Description,
	p.ModifyDate,
	p.KontragentSettlementAccountId,
	p.IsPaid,
	p.TaxationSystemType,
	p.PatentId,
	p.NdsSum,
	p.NdsType,
	p.IncludeNds,
	p.KontragentAccountCode,
	(select TotalCount from TotalCountCTE) as TotalCount
from MainData p
order by p.DocumentBaseId desc 
offset @Offset rows fetch next @limit rows only;
