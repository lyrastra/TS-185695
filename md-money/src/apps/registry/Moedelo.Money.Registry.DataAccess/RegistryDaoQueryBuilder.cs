using System;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.Registry.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Registry.Domain.Models.SelfCostPayments;

namespace Moedelo.Money.Registry.DataAccess
{
    static class RegistryDaoQueryBuilder
    {
        private static IEnumerable<OperationState> HiddenOperationStates => new[]
        {
            OperationState.ImportProcessing,
            OperationState.DuplicateProcessing,
            OperationState.MissingKontragentProcessing,
            OperationState.MissingWorkerProcessing,
            OperationState.Invalid
        };

        private static readonly IReadOnlyList<int> HiddenOperationStatesAsInt = HiddenOperationStates.Cast<int>().ToArray();

        public static QueryObject Get(string sqlTemplate, int firmId, RegistryQuery query)
        {
            var builder = new SqlQueryBuilder(sqlTemplate);
            builder.IncludeLineIf(query.AfterDate.HasValue ? "AfterDate" : "PeriodFilter", true);
            builder.IncludeLineIf("TaxationSystemType", query.TaxationSystemType.HasValue);
            builder.IncludeLineIf("SourceFilter", query.OperationSource.HasValue);
            builder.IncludeLineIf("ContractorFilter", query.ContractorId != null);
            builder.IncludeLineIf("QueryFilter", !string.IsNullOrEmpty(query.Query));

            var haveOperationTypes = query.OperationTypes != null && query.OperationTypes.Any(); 
            builder.IncludeLineIf("OperationTypes", haveOperationTypes);

            var haveDocumentBaseIds = query.DocumentBaseIds != null && query.DocumentBaseIds.Any();
            builder.IncludeLineIf("DocumentBaseIdsFilter", haveDocumentBaseIds);

            var param = new
            {
                firmId,
                query.StartDate,
                query.EndDate,
                query.Offset,
                query.Limit,
                TaxationSystemType = (int?)query.TaxationSystemType,
                KontragentTypeKontragent = ContractorType.Kontragent,
                KontragentTypeWorker = ContractorType.Worker,
                KontragentTypeAll = 0,
                ContractorId = query.ContractorId,
                ContractorType = query.ContractorType,
                SourceSettlementAccount = OperationSource.SettlementAccount,
                SourceCashbox = OperationSource.Cashbox,
                SourcePurse = OperationSource.Purse,
                Source = query.OperationSource,
                OperationStateDefault = OperationState.Default,
                query.AfterDate,
                DirectionOutgoing = MoneyDirection.Outgoing,
                PayedStatus = PaymentStatus.Payed,
                Query = query.Query,
                BadStates = HiddenOperationStatesAsInt,
            };

            var tempTableList = new List<TemporaryTable>();

            if (haveOperationTypes)
            {
                tempTableList.Add( query.OperationTypes.Cast<int>().ToTempIntIds("OperationTypes"));
            }

            if (haveDocumentBaseIds)
            {
                tempTableList.Add( query.DocumentBaseIds.ToTempBigIntIds("DocumentBaseIds"));
            }

            return new QueryObject(builder.ToString(), param, null, tempTableList);
        }
        
        public static QueryObject GetOutgoingPaymentsForTaxWidgets(string sqlTemplate, int firmId, DateTime startDate, DateTime endDate)
        {
            var builder = new SqlQueryBuilder(sqlTemplate);

            var param = new
            {
                firmId,
                startDate,
                endDate,
                OperationStateDefault = OperationState.Default,
            };

            var tempTableList = new List<TemporaryTable>()
            {
                HiddenOperationStates.Cast<int>().ToTempIntIds("BadStates")
            };

            var operationTypes = new[]
            {
                OperationType.PaymentOrderOutgoingPaymentToSupplier,
                OperationType.CashOrderOutgoingPaymentSuppliersForGoods,
            };

            tempTableList.Add(operationTypes.Cast<int>().ToTempIntIds("OperationTypes"));

            return new QueryObject(builder.ToString(), param, null, tempTableList);
        }
        
        public static QueryObject GetBankSelfCostPayments(string sqlTemplate, int firmId, SelfCostPaymentRequest request)
        {
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLineIf("StartDateFilter", request.StartDate.HasValue)
                .ToString();
            
            var operationTypes = new[]
            {
                (int) OperationType.PaymentOrderOutgoingPaymentToSupplier,
                (int) OperationType.PaymentOrderOutgoingPaymentToAccountablePerson,
                (int) OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier
            };

            var param = new
            {
                firmId,
                offset = request.Offset,
                limit = request.Limit,
                startDate = request.StartDate,
                endDate = request.EndDate,
                paidStatus = PaymentStatus.Payed,
                operationStateDefault = OperationState.Default
            };

            var tempTableList = new List<TemporaryTable>
            {
                HiddenOperationStates.Cast<int>().ToTempIntIds("badStates"),
                operationTypes.ToTempIntIds("operationTypes")
            };

            return new QueryObject(sql, param, null, tempTableList);
        }
        
        public static QueryObject GetCashSelfCostPayments(string sqlTemplate, int firmId, SelfCostPaymentRequest request)
        {
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLineIf("StartDateFilter", request.StartDate.HasValue)
                .ToString();
            
            var operationTypes = new[]
            {
                (int) OperationType.CashOrderOutgoingPaymentSuppliersForGoods,
                (int) OperationType.CashOrderOutgoingIssuanceAccountablePerson
            };

            var param = new
            {
                firmId,
                offset = request.Offset,
                limit = request.Limit,
                startDate = request.StartDate,
                endDate = request.EndDate
            };

            var tempTableList = new List<TemporaryTable>
            {
                operationTypes.ToTempIntIds("operationTypes")
            };

            return new QueryObject(sql, param, null, tempTableList);
        }
    }
}
