set nocount on;
set tran isolation level read uncommitted;

set @StartDate = iif(@StartDate < @InitialDate, @InitialDate, isnull(@StartDate, @InitialDate));
declare @StartSummaryDate date = iif(@StartDate < @InitialSummaryDate , @InitialSummaryDate , isnull(@StartDate, @InitialSummaryDate));
declare @TurnoverStartDate date = iif(@StartSummaryDate < @TodayStart, @StartSummaryDate, @TodayStart);
declare @TurnoverEndDate datetime = iif(@EndDate < @TodayEnd, @EndDate, @TodayEnd);

/* BudgetaryTypeFilter
select distinct
	ParentDocumentId,
	KbkNumber.AccountCode AccountCode
into #UnifiedTaxPayments
from
	dbo.UnifiedTaxPayment 
	join dbo.KbkNumber on KbkNumber.Id = UnifiedTaxPayment.KbkNumberId
where 
	FirmId =  @FirmId
	and KbkNumber.AccountCode = @BudgetaryType;
BudgetaryTypeFilter */


/* BudgetaryTypeExtraFilter
select
	KbkNumber.Id
into #Kbk
from
	dbo.KbkNumber 
where
	KbkNumber.AccountCode >= @StartAccountCode and KbkNumber.AccountCode < @EndAccountCode
	and KbkNumber.KbkType 
	/* AllInsuranceFeeFilter
	not 
	AllInsuranceFeeFilter */
	in (select Id from @FixedFeesKbkTypes)

select distinct
	ParentDocumentId
into #UnifiedTaxPayments
from
	dbo.UnifiedTaxPayment 
	join #Kbk on #Kbk.Id = UnifiedTaxPayment.KbkNumberId
where 
	FirmId =  @FirmId;
BudgetaryTypeExtraFilter */


with	operations
			as (select op.Id,
						op.DocumentBaseId,
						op.Date,
			            op.ModifyDate,
						op.PaymentNumber as Number,
						op.Direction,
						op.SettlementAccountId as SourceId,
						@SourceTypeSettlementAccount as SourceType,
						kontragent.Id as KontragentId,
						kontragent.Name as KontragentName,
						kontragent.Type as KontragentType,
						op.OperationType,
						op.Sum,
						op.PaidStatus,
						sa.id_currency as Currency,
						sa.SettlementAccountType as SettlementType,
						op.Description,
						ldb.TaxStatus,
						iif(op.OperationState = @ImportedOperationState, 1, 0) as IsImported
					from dbo.Accounting_PaymentOrder as op
						join dbo.AccountingLinkedDocumentBase as ldb
							on ldb.Id = op.DocumentBaseId
						outer apply
							(select top 1 DocumentBaseId,
								WorkerId
								from dbo.WorkerPayment
								where FirmId = @FirmId
								and DocumentBaseId = ldb.Id
									/* KontragentTypeFilter
									and @KontragentTypeWorker = @KontragentType
									KontragentTypeFilter */
									/* KontragentIdFilter
									and WorkerId = @KontragentId
									KontragentIdFilter */) as wp
						join dbo.SettlementAccount as sa
							on sa.id = op.SettlementAccountId
						outer apply
							(select coalesce(op.KontragentId, op.SalaryWorkerId, wp.WorkerId) as Id,
									op.KontragentName as Name,
									case	when op.KontragentId is not null then @KontragentTypeKontragent
											when isnull(op.SalaryWorkerId, wp.WorkerId) is not null then @KontragentTypeWorker
											else @KontragentTypeAll
									end as Type) as kontragent
						/* BudgetaryTypeFilter
						left join #UnifiedTaxPayments on #UnifiedTaxPayments.ParentDocumentId = op.DocumentBaseId
						BudgetaryTypeFilter */
						/* BudgetaryTypeExtraFilter
						left join #Kbk on #Kbk.Id = op.KbkId
						left join #UnifiedTaxPayments on #UnifiedTaxPayments.ParentDocumentId = op.DocumentBaseId
						BudgetaryTypeExtraFilter */
					where op.FirmId = @FirmId
						and op.Date between @InitialDate and @EndDate
						and isnull(op.OperationState, @RegularOperationState) in @OperationStates
						and op.OutsourceState is null
						/* IsApprovedOperationStateFilter
						and case when @IsApproved is null then 1 else iif(op.Date >= @InitialIsApprovedDate, 1, 0) end = 1
						IsApprovedOperationStateFilter */
						/* SourceTypeFilter
						and @SourceTypeSettlementAccount = @SourceType
						SourceTypeFilter */
						/* SourceIdFilter
						and op.SettlementAccountId = @SourceId
						SourceIdFilter */
						/* KontragentTypeFilter
						and kontragent.Type = @KontragentType
						KontragentTypeFilter */
						/* KontragentIdFilter
						and kontragent.Id = @KontragentId
						KontragentIdFilter */
						/* DirectionFilter
						and op.Direction = @Direction
						DirectionFilter */
						/* OperationTypeFilter
						and op.OperationType in (select Id from @OperationTypes)
						OperationTypeFilter */
						/* BudgetaryTypeFilter
						and (op.BudgetaryTaxesAndFees = @BudgetaryType or #UnifiedTaxPayments.AccountCode = @BudgetaryType)
						BudgetaryTypeFilter */
						/* BudgetaryTypeExtraFilter
						and (#Kbk.Id is not null or #UnifiedTaxPayments.ParentDocumentId is not null)
						BudgetaryTypeExtraFilter */
						/* SumRangeFilter
						and op.Sum between @SumFrom and @SumTo
						SumRangeFilter */
						/* SumLessFilter
						and op.Sum < @Sum
						SumLessFilter */
						/* SumEqualFilter
						and op.Sum = @Sum
						SumEqualFilter */
						/* SumGreatFilter
						and op.Sum > @Sum
						SumGreatFilter */
						/* TaxationSystemFilter
						and op.TaxationSystemType = @TaxationSystemType
						TaxationSystemFilter */
						/* PatentFilter
						and op.PatentId = @PatentId
						PatentFilter */
						/* ProvideInTaxFilter
						and isnull(ldb.TaxStatus, @TaxPostingStatusNotTax) in (select Id from @TaxPostingStatuses)
						ProvideInTaxFilter */
						/* QueryFilter
						 and ((kontragent.Name like '%!' + @Query + '%' escape '!' or op.Description like '%!' + @Query + '%' escape '!' or op.PaymentNumber like '%!' + @Query + '%' escape '!') or try_cast(replace(replace(@Query, ',', '.'), ' ', '') as decimal(20, 2)) = convert(decimal(20, 2), op.Sum))
						QueryFilter */
				union all
				select op.Id,
						op.DocumentBaseId,
						op.Date,
                        op.ModifyDate,
						op.Number,
						op.Direction,
						op.CashId as SourceId,
						@SourceTypeCash as SourceType,
						kontragent.Id as KontragentId,
						kontragent.Name as KontragentName,
						kontragent.Type as KontragentType,
						op.OperationType,
						op.Sum,
						@PayedDocumentStatus as PaidStatus,
						@CurrencyRub as Currency,
						0 as SettlementType,
						op.Destination as Description,
						ldb.TaxStatus,
						0 as IsImported
					from dbo.Accounting_CashOrder op
						join dbo.AccountingLinkedDocumentBase as ldb
							on ldb.Id = op.DocumentBaseId
						outer apply
							(select top 1
								CashOrderId,
								WorkerId,
								(select COUNT(Id) from dbo.CashOrderWorkerPayItem where CashOrderId = op.Id) as WorkersCount
								from dbo.CashOrderWorkerPayItem
								where CashOrderId = op.Id
									/* KontragentTypeFilter
									and @KontragentTypeWorker = @KontragentType
									KontragentTypeFilter */
									/* KontragentIdFilter
									and WorkerId = @KontragentId
									KontragentIdFilter */) as wpi
						outer apply
							(select coalesce(op.KontragentId, op.SalaryWorkerId, wpi.WorkerId) as Id,
									case	when wpi.WorkerId is not null and wpi.WorkersCount > 1
											and op.PaybillNumber is not null  then 'ведомость №' + op.PaybillNumber + ' от ' + convert(varchar, op.PaybillDate, 104)
										else op.DestinationName
									end as Name,
									case	when op.KontragentId is not null then @KontragentTypeKontragent
											when isnull(op.SalaryWorkerId, wpi.WorkerId) is not null then @KontragentTypeWorker
											else @KontragentTypeAll
									end as Type) as kontragent
						outer apply (select op.Destination as Content) as description
						/* BudgetaryTypeFilter
						left join #UnifiedTaxPayments on #UnifiedTaxPayments.ParentDocumentId = op.DocumentBaseId
						BudgetaryTypeFilter */
						/* BudgetaryTypeExtraFilter
						left join #Kbk on #Kbk.Id = op.KbkId
						left join #UnifiedTaxPayments on #UnifiedTaxPayments.ParentDocumentId = op.DocumentBaseId
						BudgetaryTypeExtraFilter */
					where op.FirmId = @FirmId
						and op.Date between @InitialDate and @EndDate
						/* IsApprovedOperationStateFilter
						and case when @IsApproved is null then 1 else 0 end = 1
						IsApprovedOperationStateFilter */
						/* SourceTypeFilter
						and @SourceTypeCash = @SourceType
						SourceTypeFilter */
						/* SourceIdFilter
						and op.CashId = @SourceId
						SourceIdFilter */
						/* KontragentTypeFilter
						and kontragent.Type = @KontragentType
						KontragentTypeFilter */
						/* KontragentIdFilter
						and kontragent.Id = @KontragentId
						KontragentIdFilter */
						/* DirectionFilter
						and op.Direction = @Direction
						DirectionFilter */
						/* OperationTypeFilter
						and op.OperationType in (select Id from @OperationTypes)
						OperationTypeFilter */
						/* BudgetaryTypeFilter
						and (op.BudgetaryTaxesAndFees = @BudgetaryType or #UnifiedTaxPayments.AccountCode = @BudgetaryType)
						BudgetaryTypeFilter */
						/* BudgetaryTypeExtraFilter
						and (#Kbk.Id is not null or #UnifiedTaxPayments.ParentDocumentId is not null)
						BudgetaryTypeExtraFilter */
						/* SumRangeFilter
						and op.Sum between @SumFrom and @SumTo
						SumRangeFilter */
						/* SumLessFilter
						and op.Sum < @Sum
						SumLessFilter */
						/* SumEqualFilter
						and op.Sum = @Sum
						SumEqualFilter */
						/* SumGreatFilter
						and op.Sum > @Sum
						SumGreatFilter */
						/* TaxationSystemFilter
						and op.TaxationSystemType = @TaxationSystemType
						TaxationSystemFilter */
						/* PatentFilter
						and op.PatentId = @PatentId
						PatentFilter */
						/* ProvideInTaxFilter
						and isnull(ldb.TaxStatus, @TaxPostingStatusNotTax) in (select Id from @TaxPostingStatuses)
						ProvideInTaxFilter */
						/* QueryFilter
						 and ((kontragent.Name like '%!' + @Query + '%' escape '!' or op.Destination like '%!' + @Query + '%' escape '!' or op.Number like '%!' + @Query + '%' escape '!') or try_cast(replace(replace(@Query, ',', '.'), ' ', '') as decimal(20, 2)) = convert(decimal(20, 2), op.Sum))
						QueryFilter */
				union all
				select op.Id,
						op.DocumentBaseId,
						op.DocumentDate as Date,
						null as ModifyDate,
						op.DocumentNumber as Number,
						op.Direction,
						op.PurseId as SourceId,
						@SourceTypePurse as SourceType,
						kontragent.Id as KontragentId,
						kontragent.Name as KontragentName,
						kontragent.Type as KontragentType,
						op.OperationType,
						op.Sum,
						@PayedDocumentStatus as PaidStatus,
						@CurrencyRub as Currency,
						0 as SettlementType,
						op.Comment as Description,
						ldb.TaxStatus,
						0 as IsImported
					from docs.PurseOperation as op
						join dbo.AccountingLinkedDocumentBase as ldb
							on ldb.Id = op.DocumentBaseId
						left join dbo.Kontragent as k
							on k.id = op.KontragentId
						outer apply
							(select k.Id,
									coalesce(k.ShortName, k.Fio) as Name,
									iif(op.KontragentId is not null, @KontragentTypeKontragent, @KontragentTypeAll) as Type) as kontragent
					where op.FirmId = @FirmId
						and op.DocumentDate between @InitialDate and @EndDate
						/* IsApprovedOperationStateFilter
						and case when @IsApproved is null then 1 else 0 end = 1
						IsApprovedOperationStateFilter */
						/* SourceTypeFilter
						and @SourceTypePurse = @SourceType
						SourceTypeFilter */
						/* SourceIdFilter
						and op.PurseId = @SourceId
						SourceIdFilter */
						/* KontragentTypeFilter
						and kontragent.Type = @KontragentType
						KontragentTypeFilter */
						/* KontragentIdFilter
						and kontragent.Id = @KontragentId
						KontragentIdFilter */
						/* DirectionFilter
						and op.Direction = @Direction
						DirectionFilter */
						/* OperationTypeFilter
						and op.OperationType in (select Id from @OperationTypes)
						OperationTypeFilter */
						/* SumRangeFilter
						and op.Sum between @SumFrom and @SumTo
						SumRangeFilter */
						/* SumLessFilter
						and op.Sum < @Sum
						SumLessFilter */
						/* SumEqualFilter
						and op.Sum = @Sum
						SumEqualFilter */
						/* SumGreatFilter
						and op.Sum > @Sum
						SumGreatFilter */
						/* TaxationSystemFilter
						and op.TaxSystemType = @TaxationSystemType
						TaxationSystemFilter */
						/* ProvideInTaxFilter
						and isnull(ldb.TaxStatus, @TaxPostingStatusNotTax) in (select Id from @TaxPostingStatuses)
						ProvideInTaxFilter */
						/* QueryFilter
						 and ((kontragent.Name like '%!' + @Query + '%' escape '!' or op.Comment like '%!' + @Query + '%' escape '!' or op.DocumentNumber like '%!' + @Query + '%' escape '!') or try_cast(replace(replace(@Query, ',', '.'), ' ', '') as decimal(20, 2)) = convert(decimal(20, 2), op.Sum))
						QueryFilter */),
		/* ClosedDocsCte
		closingDocs
			as (select lod.LinkedToId as DocumentBaseId,
						isnull(sum(lod.Sum), 0) as ClosingSum
					from dbo.AccountingLinkedDocumentBase as ldb
						join dbo.AccountingLinkOfDocuments as lod
							on lod.LinkedFromId = ldb.Id
					where ldb.FirmId = @FirmId
						and ldb.Type in (select Id from @DocumentTypes)
						and lod.Type = @LinkWithPayment
					group by lod.LinkedToId),
		ClosedDocsCte */
		/* --MultiCurrencyCondition--
		currencies as (select distinct Currency from operations),
		--MultiCurrencyCondition-- */
		summary
			as (select
						/* --MultiCurrencyCondition--
						currencies.Currency AS Currency,
						--MultiCurrencyCondition-- */
						operations.SettlementType as SettlementType,
						isnull(count(case	when operations.Date between @StartDate and @EndDate
												and operations.OperationType <> @IncomingBalanceOperationType
												and operations.IsImported = 0 then 1
									else null
								end), 0) as TotalCount,
						isnull(sum(case	when operations.Direction = @IncomingDirection
																and operations.Date >= @InitialSummaryDate
																and operations.Date < @StartSummaryDate then operations.Sum
														when operations.Direction = @OutgoingDirection
																and operations.PaidStatus = @PayedDocumentStatus
																and operations.Date >= @InitialSummaryDate 
																and operations.Date < @StartSummaryDate then -operations.Sum
														else 0
													end), 0) as StartBalance,
						isnull(sum(case	when operations.Direction = @IncomingDirection
																and operations.Date between @InitialSummaryDate and @TurnoverEndDate then operations.Sum
														when operations.Direction = @OutgoingDirection
																and operations.PaidStatus = @PayedDocumentStatus
																and operations.Date between @InitialSummaryDate and @TurnoverEndDate then -operations.Sum
														else 0
													end), 0) as EndBalance,
						isnull(count(case	when operations.Direction = @IncomingDirection
													and operations.Date between @TurnoverStartDate and @TurnoverEndDate
													and operations.OperationType <> @IncomingBalanceOperationType then 1
											else null
										end), 0) as IncomingCount,
						isnull(sum(case	when operations.Direction = @IncomingDirection
													and operations.Date between @TurnoverStartDate and @TurnoverEndDate
													and operations.OperationType <> @IncomingBalanceOperationType then operations.Sum
										else 0
									end), 0) as IncomingBalance,
						@StartSummaryDate as IncomingDate,
						isnull(count(case	when operations.Direction = @OutgoingDirection
													and operations.PaidStatus = @PayedDocumentStatus
													and operations.Date between @TurnoverStartDate and @TurnoverEndDate then 1
											else null
										end), 0) as OutgoingCount,
						isnull(sum(case	when operations.Direction = @OutgoingDirection
												and operations.PaidStatus = @PayedDocumentStatus
												and operations.Date between @TurnoverStartDate and @TurnoverEndDate then operations.Sum
										else 0
									end), 0) as OutgoingBalance,
						@TurnoverEndDate as OutgoingDate
					from operations
								/* --MultiCurrencyCondition--
									JOIN currencies ON currencies.Currency = operations.Currency
								--MultiCurrencyCondition-- */
						/* ClosedDocsJoin
						left join closingDocs
							on closingDocs.DocumentBaseId = operations.DocumentBaseId
						ClosedDocsJoin */
						/* ClosedDocsWhere
						where 
						operations.OperationType in (select id from @ClosingDocumentsOperationType)
						and case when isnull(closingDocs.ClosingSum, 0) = 0 then @ClosingDocumentsConditionNo
							when isnull(closingDocs.ClosingSum, 0) < operations.Sum then @ClosingDocumentsConditionPartly
							when isnull(closingDocs.ClosingSum, 0) >= operations.Sum then @ClosingDocumentsConditionCompletely
						end = @ClosingDocumentsCondition
						ClosedDocsWhere */
						GROUP BY operations.SettlementType
						/* --MultiCurrencyCondition--
							,currencies.Currency
						--MultiCurrencyCondition-- */)
	select 1 as isSummary,
			0 as Id,
			0 as DocumentBaseId,
			null as Date,
			null as ModifyDate,
			'' as Number,
			0 as Direction,
			null as KontragentId,
			null as KontragentName,
			@KontragentTypeAll as KontragentType,
			0 as OperationType,
			0 as PaidStatus,
			0 as Sum,
		/* --NotMultiCurrencyCondition--
			@CurrencyRub as Currency,
			--NotMultiCurrencyCondition-- */
		/* --MultiCurrencyCondition--
			summary.Currency as Currency,
			--MultiCurrencyCondition-- */
			SettlementType,
			null as Description,
			summary.IncomingCount,
			summary.IncomingBalance,
			summary.IncomingDate,
			summary.OutgoingCount,
			summary.OutgoingBalance,
			summary.OutgoingDate,
			summary.StartBalance,
			summary.EndBalance,
			summary.TotalCount
		from summary
	union all
	select 0,
			Id,
			DocumentBaseId,
			Date,
            ModifyDate,
			Number,
			Direction,
			KontragentId,
			KontragentName,
			KontragentType,
			OperationType,
			PaidStatus,
			Sum,
			Currency,
			SettlementType,
			Description,
			0,
			0,
			null,
			0,
			0,
			null,
			0,
			0,
			0
		from (select operations.Id,
						operations.DocumentBaseId,
						operations.Date,
		                operations.ModifyDate,
						operations.Number,
						operations.Direction,
						operations.KontragentId,
						operations.KontragentName,
						operations.KontragentType,
						operations.OperationType,
						operations.PaidStatus,
						operations.Sum,
						operations.Currency,
						operations.SettlementType,
						operations.Description
					from operations
						/* ClosedDocsJoin
						left join closingDocs
							on closingDocs.DocumentBaseId = operations.DocumentBaseId
						ClosedDocsJoin */
					where operations.Date between @StartDate and @EndDate
						and operations.OperationType <> @IncomingBalanceOperationType
						and operations.IsImported = 0
						/* ClosedDocsWhere
						and operations.OperationType in (select id from @ClosingDocumentsOperationType)
						and case	when isnull(closingDocs.ClosingSum, 0) = 0 then @ClosingDocumentsConditionNo
							when isnull(closingDocs.ClosingSum, 0) < operations.Sum then @ClosingDocumentsConditionPartly
							when isnull(closingDocs.ClosingSum, 0) >= operations.Sum then @ClosingDocumentsConditionCompletely
						end = @ClosingDocumentsCondition
						ClosedDocsWhere */
					order by
						/* SortByDocumentBaseId
						DocumentBaseId
						SortByDocumentBaseId */
						/* SortByDate
						Date
						SortByDate */
						/* SortByKontragentName
						KontragentName
						SortByKontragentName */
						/* SortBySum
						Sum
						SortBySum */
						/* SortOrderDesc
						desc
						SortOrderDesc */
					offset @Offset rows
					fetch next @Count rows only) as ops;

/* BudgetaryTypeFilter
drop table #UnifiedTaxPayments
BudgetaryTypeFilter */

/* BudgetaryTypeExtraFilter
drop table #Kbk
drop table #UnifiedTaxPayments
BudgetaryTypeExtraFilter */
