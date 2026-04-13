using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.Finances.Domain.Enums;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Enums.Money.Table;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using Moedelo.Finances.Domain.Models.Money.Table.Filters;

namespace Moedelo.Finances.DataAccess.Money.Tables
{
    internal static class MoneyTableSqlBuilder
    {
        private static readonly IReadOnlyCollection<int> FixedFeesKbkTypes = new [] { 5, 9, 11, 42 };
        private static readonly OperationState[] UnrecognizedOperationStateList = UnrecognizedOperationStates.List;

        internal static QueryObjectWithDynamicParams CreateGetUnrecognizedTableQueryObject(int firmId, DateTime initialDate, MoneyTableRequest request)
        {
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
                new DynamicParam("InitialDate", initialDate.Date, DbType.Date),
                new DynamicParam(nameof(request.Offset), request.Offset),
                new DynamicParam(nameof(request.Count), request.Count),
                new DynamicParam("RegularOperationState", OperationState.Default),
                new DynamicParam("UnrecognizedOperationStates", UnrecognizedOperationStateList.Cast<int>().ToIntListTVP()),
                new DynamicParam(nameof(request.SourceId), request.SourceType == MoneySourceType.SettlementAccount ? request.SourceId : null),
                new DynamicParam("UnconfirmedOutsourceState", OutsourceState.Unconfirmed),
            };
            return new QueryObjectWithDynamicParams(TableQueries.GetUnrecognized, sqlParams);
        }

        internal static QueryObject CreateGetUnrecognizedOperationCountQueryObject(int firmId, DateTime initialDate, MoneySourceType sourceType, long? sourceId)
        {
            var sqlParams = new
            {
                FirmId = firmId,
                InitialDate = initialDate.Date,
                SourceId = sourceType != MoneySourceType.All ? sourceId : null,
                RegularOperationState = OperationState.Default,
                UnrecognizedOperationStates = UnrecognizedOperationStateList.Cast<int>().ToIntListTVP(),
                UnconfirmedOutsourceState = OutsourceState.Unconfirmed,
            };
            return new QueryObject(TableQueries.GetUnrecognizedOperationCount, sqlParams);
        }

        internal static QueryObjectWithDynamicParams CreateGetImportedTableQueryObject(
            int firmId,
            DateTime initialDate,
            MoneyTableRequest request)
        {
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
                new DynamicParam("InitialDate", initialDate.Date, DbType.Date),
                new DynamicParam(nameof(request.Offset), request.Offset),
                new DynamicParam(nameof(request.Count), request.Count),
                new DynamicParam("RegularOperationState", OperationState.Default),
                new DynamicParam("ImportedOperationState", OperationState.Imported),
                new DynamicParam("UnconfirmedOutsourceState", OutsourceState.Unconfirmed),
                new DynamicParam(nameof(request.SourceId), request.SourceType == MoneySourceType.SettlementAccount ? request.SourceId : null)
            };
            return new QueryObjectWithDynamicParams(TableQueries.GetImported, sqlParams);
        }

        public static QueryObject CreateGetImportedOperationsCountQueryObject(int firmId, DateTime initialDate,
            MoneySourceType sourceType, long? sourceId)
        {
            var sqlParams = new
            {
                FirmId = firmId,
                InitialDate = initialDate.Date,
                DbType.Date,
                SourceId = sourceType != MoneySourceType.All ? sourceId : null,
                RegularOperationState = OperationState.Default,
                ImportedOperationState = OperationState.Imported,
                UnconfirmedOutsourceState = OutsourceState.Unconfirmed,
            };
            return new QueryObject(TableQueries.GetImportedOperationCount, sqlParams);
        }

        internal static QueryObjectWithDynamicParams CreateGetOutsourceProcessingTableQueryObject(
            int firmId,
            DateTime initialDate,
            OutsourceProcessingTableRequest request,
            bool onlyTotalCount = false)
        {
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
                new DynamicParam(nameof(request.Offset), request.Offset),
                new DynamicParam(nameof(request.Count), request.Count),
                new DynamicParam("UnconfirmedOutsourceState", OutsourceState.Unconfirmed),
                new DynamicParam(nameof(request.StartDate), new[] { initialDate.Date, request.StartDate ?? SqlDateTime.MinValue.Value.Date }.Max(), DbType.Date),
                new DynamicParam(nameof(request.EndDate), request.EndDate?.Date.Add(new TimeSpan(23, 59, 59)) ?? SqlDateTime.MaxValue.Value.Date, DbType.DateTime),
                new DynamicParam("KontragentTypeAll", MoneyContractorType.All),
                new DynamicParam("KontragentTypeKontragent", MoneyContractorType.Kontragent),
                new DynamicParam("KontragentTypeWorker", MoneyContractorType.Worker),
            };
            var sql = new StringBuilder(TableQueries.GetOutsourceProcessing);
            
            MakeSourceFilter(sql, sqlParams, request);
            // упрощение для "Выплат физ. лицам": не ищем в подплатежах - импорт их не заполняет (более корректная версия в GetMain.sql)
            MakeKontragentFilter(sql, sqlParams, request);
            MakeDirectionFilter(sql, sqlParams, request);
            // фильтр по подтипу БП в большинстве случаев некорректен (для ЕНП импорт не заполняет подплатежи)
            MakeOperationTypeFilter(sql, sqlParams, request);
            MakeSumFilter(sql, sqlParams, request);
            MakeQueryFilter(sql, sqlParams, request);

            if (onlyTotalCount)
            {
                sql.Replace("--NotSelectPage--", string.Empty);
            }
            
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        internal static QueryObjectWithDynamicParams CreateGetMainTableQueryObject(int firmId, DateTime initialDate,
        DateTime initialSummaryDate, MainMoneyTableRequest request)
        {
            var today = DateTime.Today;
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
                new DynamicParam("TodayStart", today, DbType.Date),
                new DynamicParam("TodayEnd", new DateTime(today.Year, today.Month, today.Day, 23, 59, 59), DbType.DateTime),
                new DynamicParam("FirmId", firmId),
                new DynamicParam("InitialDate", initialDate.Date, DbType.Date),
                new DynamicParam("InitialSummaryDate", initialSummaryDate.Date, DbType.Date),
                new DynamicParam(nameof(request.Offset), request.Offset),
                new DynamicParam(nameof(request.Count), request.Count),
                new DynamicParam(nameof(request.StartDate), request.StartDate?.Date ?? SqlDateTime.MinValue.Value.Date, DbType.Date),
                new DynamicParam(nameof(request.EndDate), request.EndDate?.Date.AddHours(23).AddMinutes(59).AddSeconds(59) ?? SqlDateTime.MaxValue.Value.Date, DbType.DateTime),
                new DynamicParam("SourceTypeSettlementAccount", MoneySourceType.SettlementAccount),
                new DynamicParam("SourceTypeCash", MoneySourceType.Cash),
                new DynamicParam("SourceTypePurse", MoneySourceType.Purse),
                new DynamicParam("KontragentTypeAll", MoneyContractorType.All),
                new DynamicParam("KontragentTypeKontragent", MoneyContractorType.Kontragent),
                new DynamicParam("KontragentTypeWorker", MoneyContractorType.Worker),
                new DynamicParam("CurrencyRub", Currency.RUB),
                new DynamicParam("OutgoingDirection", MoneyDirection.Outgoing),
                new DynamicParam("IncomingDirection", MoneyDirection.Incoming),
                new DynamicParam("PayedDocumentStatus", DocumentStatus.Payed),
                new DynamicParam("RegularOperationState", OperationState.Default),
                new DynamicParam("ImportedOperationState", OperationState.Imported),
                new DynamicParam("NoAutoDeleteOperationState", OperationState.NoAutoDelete),
                new DynamicParam("OutsourceApprovedOperationState", OperationState.OutsourceApproved),
                new DynamicParam("IncomingBalanceOperationType", OperationType.PaymentOrderIncomingBalance),
                new DynamicParam("UnconfirmedOutsourceState", OutsourceState.Unconfirmed),
            };
            var sql = new StringBuilder(TableQueries.GetMain);
            sql.ApplyFilter(sqlParams, request);
            sql.ApplyClosedDocumentsCte(request);
            sql.ApplyClosedDocumentsJoin(request);
            sql.ApplyClosedDocumentsWhere(request);
            sql.ApplySortOrder(request);
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        private static void ApplyFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, MainMoneyTableRequest request)
        {
            query.MakeOperationStateFilter(sqlParams, request);
            query.MakeSourceFilter(sqlParams, request);
            query.MakeKontragentFilter(sqlParams, request);
            query.MakeDirectionFilter(sqlParams, request);
            query.MakeOperationTypeFilter(sqlParams, request);
            query.MakeSumFilter(sqlParams, request);
            query.MakeTaxationSystemType(sqlParams, request);
            query.MakeProvideInTaxFilter(sqlParams, request);
            query.MakeClosingDocumentsFilter(sqlParams, request);
            query.MakeQueryFilter(sqlParams, request);
            query.MakeMultiCurrency(request);
        }

        private static void MakeTaxationSystemType(this StringBuilder query, ICollection<DynamicParam> sqlParams, MainMoneyTableRequest request)
        {
            if (request.TaxationSystemType.HasValue)
            {
                sqlParams.Add(new DynamicParam(nameof(request.TaxationSystemType), request.TaxationSystemType));
                query.Uncomment("TaxationSystemFilter");
            }

            if (request.PatentId.HasValue)
            {
                sqlParams.Add(new DynamicParam(nameof(request.PatentId), request.PatentId));
                query.Uncomment("PatentFilter");
            }
        }

        private static void MakeMultiCurrency(this StringBuilder query, MainMoneyTableRequest request)
        {
            if (request.IsMultiCurrency)
            {
                query.Uncomment("--MultiCurrencyCondition--");
            }
            else
            {
                query.Uncomment("--NotMultiCurrencyCondition--");
            }
        }

        private static void MakeSourceFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, ISourceFilter request)
        {
            if (request.SourceType != MoneySourceType.All)
            {
                sqlParams.Add(new DynamicParam(nameof(request.SourceType), request.SourceType));
                query.Uncomment("SourceTypeFilter");
                if (request.SourceId.HasValue)
                {
                    sqlParams.Add(new DynamicParam(nameof(request.SourceId), request.SourceId.Value));
                    query.Uncomment("SourceIdFilter");
                }
            }
        }

        private static void MakeKontragentFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, IKontragentFilters request)
        {
            if (request.KontragentType != MoneyContractorType.All)
            {
                sqlParams.Add(new DynamicParam(nameof(request.KontragentType), request.KontragentType));
                query.Uncomment("KontragentTypeFilter");
            }
            if (request.KontragentId.HasValue)
            {
                sqlParams.Add(new DynamicParam(nameof(request.KontragentId), request.KontragentId.Value));
                query.Uncomment("KontragentIdFilter");
            }
        }

        private static void MakeDirectionFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, IDirectionFilter request)
        {
            if (request.Direction != MoneyDirection.All)
            {
                sqlParams.Add(new DynamicParam(nameof(request.Direction), request.Direction));
                query.Uncomment("DirectionFilter");
            }
        }

        private static void MakeOperationTypeFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, IOperationTypesFilters request)
        {
            if ((request.OperationTypes?.Length ?? 0) > 0)
            {
                sqlParams.Add(new DynamicParam(nameof(request.OperationTypes), request.OperationTypes.Cast<int>().ToIntListTVP()));
                query.Uncomment("OperationTypeFilter");
                if (request.OperationTypes.Contains(OperationType.BudgetaryPayment) && request.BudgetaryType.HasValue)
                {
                    sqlParams.Add(new DynamicParam(nameof(request.BudgetaryType), request.BudgetaryType.Value));
                    query.Uncomment("BudgetaryTypeFilter");
                }
                if (request.OperationTypes.Contains(OperationType.BudgetaryPayment) && request.ExtraBudgetaryType.HasValue)
                {
                    sqlParams.Add(new DynamicParam("FixedFeesKbkTypes", FixedFeesKbkTypes.ToIntListTVP()));
                    sqlParams.Add(new DynamicParam("StartAccountCode", 690000));
                    sqlParams.Add(new DynamicParam("EndAccountCode", 700000));
                    query.Uncomment("BudgetaryTypeExtraFilter");
                    if (request.ExtraBudgetaryType.Value == MoneyTableExtraBudgetaryFilterType.AllInsuranceFee)
                    {
                        query.Uncomment("AllInsuranceFeeFilter");
                    }
                }
            }
        }

        private static void MakeOperationStateFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, MainMoneyTableRequest request)
        {
            var operationStates = new List<OperationState>();

            // только обработанные аутсорсом
            if (request.IsApproved == true)
            {
                operationStates.Add(OperationState.OutsourceApproved);
            }
            // только НЕобработанные аутсорсом
            else if (request.IsApproved == false)
            {
                operationStates.Add(OperationState.Default);
                operationStates.Add(OperationState.Imported);
                operationStates.Add(OperationState.NoAutoDelete);
            }
            // все операции
            else
            {
                operationStates.Add(OperationState.Default);
                operationStates.Add(OperationState.Imported);
                operationStates.Add(OperationState.NoAutoDelete);
                operationStates.Add(OperationState.OutsourceApproved);
            }

            sqlParams.Add(new DynamicParam("OperationStates", operationStates));

            if (request.IsApproved != null)
            {
                sqlParams.Add(new DynamicParam("IsApproved", request.IsApproved));
                sqlParams.Add(new DynamicParam("InitialIsApprovedDate", new DateTime(2023, 06, 01)));
                query.Uncomment("IsApprovedOperationStateFilter");
            }
        }

        private static void MakeSumFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, ISumFilters request)
        {
            if (request.SumCondition != SumCondition.Any)
            {
                if (request.SumCondition == SumCondition.Range)
                {
                    const string sumFromName = nameof(request.SumFrom);
                    const string sumToName = nameof(request.SumTo);
                    sqlParams.Add(new DynamicParam(sumFromName, request.SumFrom ?? decimal.MinValue));
                    sqlParams.Add(new DynamicParam(sumToName, request.SumTo ?? decimal.MaxValue));
                    query.Uncomment("SumRangeFilter");
                    return;
                }

                const string paramName = "Sum";
                sqlParams.Add(new DynamicParam(paramName, request.Sum ?? 0m));
                switch (request.SumCondition)
                {
                    case SumCondition.Less:
                        query.Uncomment("SumLessFilter");
                        break;
                    case SumCondition.Equal:
                        query.Uncomment("SumEqualFilter");
                        break;
                    case SumCondition.Great:
                        query.Uncomment("SumGreatFilter");
                        break;
                }
            }
        }

        private static void MakeProvideInTaxFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, MainMoneyTableRequest request)
        {
            if (request.ProvideInTax.HasValue)
            {
                sqlParams.Add(new DynamicParam("TaxPostingStatusNotTax", TaxPostingStatus.NotTax));
                var taxPostingStatuses = request.ProvideInTax.Value
                    ? new[] { TaxPostingStatus.ByHand, TaxPostingStatus.Yes, TaxPostingStatus.ByLinkedDocument }
                    : new[] { TaxPostingStatus.NotTax, TaxPostingStatus.No };
                sqlParams.Add(new DynamicParam("TaxPostingStatuses", taxPostingStatuses.Cast<int>().ToIntListTVP()));
                query.Uncomment("ProvideInTaxFilter");
            }
        }

        private static void MakeClosingDocumentsFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, MainMoneyTableRequest request)
        {
            if (request.ClosingDocumentsCondition == ClosingDocumentsCondition.NoMatter)
            {
                return;
            }
            var documentTypes = new[]
            {
                AccountingDocumentType.Waybill,
                AccountingDocumentType.Statement,
                AccountingDocumentType.MiddlemanReport,
                AccountingDocumentType.Upd,
                AccountingDocumentType.SalesUpd,
                AccountingDocumentType.ReceiptStatement,
            };
            var closingDocumentsOperationType = new[]
            {
                OperationType.PaymentOrderIncomingPaymentForGoods,
                OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods,
                OperationType.PaymentOrderIncomingMediationFee,
                OperationType.CashOrderIncomingMediationFee
            };
            sqlParams.Add(new DynamicParam("DocumentTypes", documentTypes.Cast<int>().ToIntListTVP()));
            sqlParams.Add(new DynamicParam("ClosingDocumentsOperationType", closingDocumentsOperationType.Cast<int>().ToIntListTVP()));
            sqlParams.Add(new DynamicParam("LinkWithPayment", LinkType.Payment));

            sqlParams.Add(new DynamicParam("ClosingDocumentsConditionNo", ClosingDocumentsCondition.No));
            sqlParams.Add(new DynamicParam("ClosingDocumentsConditionPartly", ClosingDocumentsCondition.Partly));
            sqlParams.Add(new DynamicParam("ClosingDocumentsConditionCompletely", ClosingDocumentsCondition.Completely));
            sqlParams.Add(new DynamicParam(nameof(request.ClosingDocumentsCondition), request.ClosingDocumentsCondition));
        }

        private static void MakeQueryFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, IQueryFilter request)
        {
            if (!string.IsNullOrEmpty(request.Query))
            {
                sqlParams.Add(new DynamicParam(nameof(request.Query), request.Query));
                query.Uncomment("QueryFilter");
            }
        }

        private static void ApplyClosedDocumentsCte(this StringBuilder query, MainMoneyTableRequest request)
        {
            if (request.ClosingDocumentsCondition != ClosingDocumentsCondition.NoMatter)
            {
                query.Uncomment("ClosedDocsCte");
            }
        }

        private static void ApplyClosedDocumentsJoin(this StringBuilder query, MainMoneyTableRequest request)
        {
            if (request.ClosingDocumentsCondition != ClosingDocumentsCondition.NoMatter)
            {
                query.Uncomment("ClosedDocsJoin");
            }
        }

        private static void ApplyClosedDocumentsWhere(this StringBuilder query, MainMoneyTableRequest request)
        {
            if (request.ClosingDocumentsCondition != ClosingDocumentsCondition.NoMatter)
            {
                query.Uncomment("ClosedDocsWhere");
            }
        }

        private static void ApplySortOrder(this StringBuilder query, MainMoneyTableRequest request)
        {
            switch (request.SortColumn)
            {
                case MoneyTableSortSortColumn.Date:
                    query.Uncomment("SortByDate");
                    break;
                case MoneyTableSortSortColumn.KontragentName:
                    query.Uncomment("SortByKontragentName");
                    break;
                case MoneyTableSortSortColumn.Sum:
                    query.Uncomment("SortBySum");
                    break;
                default:
                    query.Uncomment("SortByDocumentBaseId");
                    break;
            }

            if (request.SortType == SortType.Desc)
            {
                query.Uncomment("SortOrderDesc");
            }
        }

        private static StringBuilder Uncomment(this StringBuilder source, string name)
        {
            return source.Replace($"/* {name}", string.Empty).Replace($"{name} */", string.Empty);
        }
    }
}
