select
	po.FirmId,
	iif(
		sum(
			case
				when po.Date < @InitialDate then 1 
				when po.Date >= @StartDate and po.OperationState = @OutsourceApprovedState then 1
				else 0
			end) = count(0),
		1, 0) as AllApproved
	from #firmIds as fids
		left join dbo.Accounting_PaymentOrder as po on
			fids.Id = po.FirmId
	where po.[Date] between @StartDate and @EndDate
	--PaidStatusFilter-- and po.PaidStatus = @PaidStatus
	group by po.FirmId;
