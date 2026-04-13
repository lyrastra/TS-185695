select po.FirmId,
		po.SettlementAccountId,
		isnull(sum(case
			when po.Direction = @IncomingDirection and isnull(po.OperationState, @DefaultOperationState) in (@DefaultOperationState, @ImportedOperationState, @OutsourceApprovedOperationState) then po.Sum
			when po.Direction = @OutgoingDirection and isnull(po.OperationState, @DefaultOperationState) in (@DefaultOperationState, @ImportedOperationState, @OutsourceApprovedOperationState) then -po.Sum
			else 0
		end), 0) as Balance,
		sum(case
			when po.Direction = @IncomingDirection and isnull(po.OperationState, @DefaultOperationState) in @UnrecognizedOperationStates then 1
			else 0
		end) as UnrecognizedIncomingCount,
		isnull(sum(iif(po.Direction = @IncomingDirection and isnull(po.OperationState, @DefaultOperationState) in @UnrecognizedOperationStates, po.Sum, 0)), 0) as UnrecognizedIncomingSum,
		sum(case
			when po.Direction = @OutgoingDirection and isnull(po.OperationState, @DefaultOperationState) in @UnrecognizedOperationStates then 1
			else 0
		end) as UnrecognizedOutgoingCount,
		isnull(sum(iif(po.Direction = @OutgoingDirection and isnull(po.OperationState, @DefaultOperationState) in @UnrecognizedOperationStates, po.Sum, 0)), 0) as UnrecognizedOutgoingSum
	from dbo.Accounting_PaymentOrder as po
		join #firmInitDates as fid
			on fid.FirmId = po.FirmId
	where po.Date between fid.InitDate and @OnDate
		and po.PaidStatus = @PayedDocumentStatus
	group by po.FirmId, po.SettlementAccountId;