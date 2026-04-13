using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Extensions.Finances.Money;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.PaymentOrders
{
    internal static class PaymentOrderOperationSqlBuilder
    {
        internal static QueryObject GetByBaseIdsQuery(
            int firmId, IReadOnlyCollection<long> baseIds)
        {
            var builder = BuildSql.From(PaymentOrderOperationQueries.Get);
            builder.IncludeBlock("BaseIdsJoin");
            var queryParams = new
            {
                firmId,
                baseIds = baseIds.ToBigIntListTVP()
            };
            return new QueryObject(builder.ToString(), queryParams);
        }

        internal static QueryObjectWithDynamicParams GetIncomingBalanceOperationQuery(int firmId, int settlementAccountId)
        {
            var sqlParams = new List<DynamicParam>();
            var sql = new StringBuilder(PaymentOrderOperationQueries.Get);
            sql.ApplyTopConstraint(sqlParams, 1);
            sql.ApplyFirmFilter(sqlParams, firmId);
            sql.ApplySettlementAccountFilter(sqlParams, settlementAccountId);
            sql.ApplyOperationTypeFilter(sqlParams, OperationType.PaymentOrderIncomingBalance);
            sql.Append("order by Id");
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        internal static QueryObjectWithDynamicParams GetForReconciliationQuery(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate)
        {
            var sqlParams = new List<DynamicParam>();
            var sql = new StringBuilder(PaymentOrderOperationQueries.Get);
            sql.ApplyFirmFilter(sqlParams, firmId);
            sql.ApplyForReconciliationOperationsFilter(sqlParams);
            sql.ApplySettlementAccountFilter(sqlParams, settlementAccountId);
            sql.ApplyPeriodFilter(sqlParams, startDate, endDate);
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        internal static QueryObjectWithDynamicParams GetFor1cConfirmationQuery(DateTime startDate, DateTime endDate)
        {
            var sqlParams = new List<DynamicParam>();
            var sql = new StringBuilder(PaymentOrderOperationQueries.Get);
            sql.JoinFirmOnService();
            sql.JoinPaymentHistoryOnService();
            sql.ApplyFor1cConfirmationOperationsFilter(sqlParams);
            sql.ApplyPeriodFilter(sqlParams, startDate, endDate);
            sql.Append("and ph.success = 1 and ph.payment_method <> 'trial'");
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        internal static QueryObjectWithDynamicParams GetBudgetaryPaymentsQuery(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var builder = BuildSql.From(PaymentOrderOperationQueries.Get);

            //Select Statement
            builder.IncludeLine("SelectKbkNumberFromSnapshot")
                   .IncludeBlock("KbkJoin");

            //Where Statement
            builder.IncludeLine("FirmIdFilter")
                   .IncludeLine("OperationType")
                   .IncludeLine("ExcludeOperationStates")
                   .IncludeLineIf("PaidStatus", queryParams.PaidStatus.HasValue)
                   .IncludeLineIf("PeriodFilter", (queryParams.StartDate.HasValue && queryParams.EndDate.HasValue))
                   .IncludeLineIf("KbkFilter", queryParams.KbkId.HasValue)
                   .IncludeLineIf("KbkPaymentType", queryParams.KbkPaymentType.HasValue)
                   .IncludeLineIf("PaymentDirection", queryParams.PaymentDirection.HasValue)
                   .IncludeLineIf("BudgetaryTaxesAndFees", queryParams.BudgetaryTaxesAndFees?.Count > 0)
                   .IncludeLineIf("BudgetaryPeriodType", queryParams.BudgetaryPeriodType.HasValue)
                   .IncludeLineIf("BudgetaryPeriodNumber", queryParams.BudgetaryPeriodNumber.HasValue)
                   .IncludeLineIf("BudgetaryPeriodYear", queryParams.BudgetaryYear.HasValue)
                   .IncludeLineIf("PatentId", queryParams.PatentId.HasValue);

            return new QueryObjectWithDynamicParams(builder.ToString(), MapBudgetaryPaymentQueryParams(firmId, queryParams));
        }

        internal static QueryObject GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var builder = BuildSql.From(PaymentOrderOperationQueries.GetWithUnifiedTaxPayments);

            //Select Statement
            builder.IncludeLine("SelectKbkNumberFromSnapshot")
                   .IncludeBlock("KbkJoin")
                   .IncludeBlock("UtpKbkJoin");

            //Where Statement
            builder.IncludeLine("FirmIdFilter")
                   .IncludeLine("OperationType")
                   .IncludeLine("ExcludeOperationStates")
                   .IncludeLineIf("PaidStatus", queryParams.PaidStatus.HasValue)
                   .IncludeLineIf("PeriodFilter", (queryParams.StartDate.HasValue && queryParams.EndDate.HasValue))
                   .IncludeLineIf("KbkFilter", queryParams.KbkId.HasValue)
                   .IncludeLineIf("KbkPaymentType", queryParams.KbkPaymentType.HasValue)
                   .IncludeLineIf("PaymentDirection", queryParams.PaymentDirection.HasValue)
                   .IncludeLineIf("BudgetaryTaxesAndFees", queryParams.BudgetaryTaxesAndFees?.Count > 0)
                   .IncludeLineIf("BudgetaryPeriodType", queryParams.BudgetaryPeriodType.HasValue)
                   .IncludeLineIf("BudgetaryPeriodNumber", queryParams.BudgetaryPeriodNumber.HasValue)
                   .IncludeLineIf("BudgetaryPeriodYear", queryParams.BudgetaryYear.HasValue)
                   .IncludeLineIf("PatentId", queryParams.PatentId.HasValue);

            return new QueryObject(builder.ToString(), MapBudgetaryPaymentsWithUnifiedTaxPaymentsQueryParams(firmId, queryParams));
        }

        private static List<DynamicParam> MapBudgetaryPaymentQueryParams(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var badOperationStates = OperationStateExtensions.BadOperationStates;
            var operationsType = new[]
            {
                OperationType.BudgetaryPayment
            };

            var sqlParams = new List<DynamicParam>();
            sqlParams.Add(new DynamicParam("FirmId", firmId));
            sqlParams.Add(new DynamicParam("OperationsType", operationsType.Cast<int>().ToIntListTVP()));
            sqlParams.Add(new DynamicParam("ExcludeOperationStates", badOperationStates.Cast<int>().ToIntListTVP()));

            sqlParams.Add(new DynamicParam("StartDate", queryParams.StartDate));
            sqlParams.Add(new DynamicParam("EndDate", queryParams.EndDate));
            sqlParams.Add(new DynamicParam("KbkId", queryParams.KbkId));
            sqlParams.Add(new DynamicParam("KbkPaymentType", queryParams.KbkPaymentType));
            sqlParams.Add(new DynamicParam("PaymentDirection", queryParams.PaymentDirection));
            sqlParams.Add(new DynamicParam("PaidStatus", queryParams.PaidStatus));
            sqlParams.Add(new DynamicParam("BudgetaryTaxesAndFees", queryParams.BudgetaryTaxesAndFees.ToIntListTVP()));
            sqlParams.Add(new DynamicParam("BudgetaryPeriodType", queryParams.BudgetaryPeriodType));
            sqlParams.Add(new DynamicParam("BudgetaryPeriodNumber", queryParams.BudgetaryPeriodNumber));
            sqlParams.Add(new DynamicParam("BudgetaryPeriodYear", queryParams.BudgetaryYear));
            sqlParams.Add(new DynamicParam("PatentId", queryParams.PatentId));

            return sqlParams;
        }

        private static object MapBudgetaryPaymentsWithUnifiedTaxPaymentsQueryParams(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var badOperationStates = OperationStateExtensions.BadOperationStates;
            var operationsTypes = new[]
            {
                OperationType.BudgetaryPayment,
                OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment
            };

            return new
            {
                firmId,
                OperationsType = operationsTypes.Cast<int>().ToIntListTVP(),
                ExcludeOperationStates = badOperationStates.Cast<int>().ToIntListTVP(),
                queryParams.StartDate,
                queryParams.EndDate,
                queryParams.KbkId,
                queryParams.KbkPaymentType,
                queryParams.PaymentDirection,
                queryParams.PaidStatus,
                BudgetaryTaxesAndFees = queryParams.BudgetaryTaxesAndFees.ToIntListTVP(),
                queryParams.BudgetaryPeriodType,
                queryParams.BudgetaryPeriodNumber,
                BudgetaryPeriodYear = queryParams.BudgetaryYear,
                queryParams.PatentId
            };
        }

        private static void JoinFirmOnService(this StringBuilder query)
        {
            query.AppendLine("join dbo.FirmOnService as fos on fos.FirmId = po.FirmId");
        }

        private static void JoinPaymentHistoryOnService(this StringBuilder query)
        {
            query.AppendLine("join dbo.PaymentHistory as ph on ph.id_firm = po.FirmId");
        }

        private static void ApplyFirmFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, int firmId)
        {
            sqlParams.Add(new DynamicParam("FirmId", firmId));
            query.Replace("--FirmIdFilter--", string.Empty); //TODO заменить на IncludeLine
        }

        private static void ApplyForReconciliationOperationsFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams)
        {
            var regularOperationStates = new[]
            {
                OperationState.Default,
                OperationState.Imported,
                OperationState.OutsourceApproved
            };
            var missingOperationStates = new[]
            {
                OperationState.MissingKontragent,
                OperationState.MissingWorker,
                OperationState.MissingExchangeRate,
                OperationState.MissingCurrencySettlementAccount,
                OperationState.MissingContract,
                OperationState.MissingCommissionAgent
            };
            sqlParams.Add(new DynamicParam("RegularOperationState", (int)OperationState.Default));
            sqlParams.Add(new DynamicParam("RegularOperationStates", regularOperationStates.Cast<int>().ToIntListTVP()));
            sqlParams.Add(new DynamicParam("MissingOperationStates", missingOperationStates.Cast<int>().ToIntListTVP()));
            sqlParams.Add(new DynamicParam("PaidStatus", (int)DocumentStatus.Payed));
            query.AppendLine("and ((isnull(po.OperationState, @RegularOperationState) in (select Id from @RegularOperationStates) and po.PaidStatus = @PaidStatus)");
            query.AppendLine("or isnull(po.OperationState, @RegularOperationState) in (select Id from @MissingOperationStates))");
        }

        private static void ApplyFor1cConfirmationOperationsFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams)
        {
            sqlParams.Add(new DynamicParam("RegularOperationState", (int)OperationState.Default));
            query.AppendLine("and (isnull(po.OperationState, @RegularOperationState) = @RegularOperationState)");
            query.AppendLine("and po.SourceFileId is not null");
        }

        private static void ApplySettlementAccountFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, int settlementAccountId)
        {
            sqlParams.Add(new DynamicParam("SettlementAccountId", settlementAccountId));
            query.AppendLine("and po.SettlementAccountId = @SettlementAccountId");
        }

        private static void ApplyOperationTypeFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, OperationType operationType)
        {
            sqlParams.Add(new DynamicParam("OperationsType", new[] { operationType }.Cast<int>().ToIntListTVP()));
            query.Replace("--OperationType--", string.Empty); //TODO заменить на IncludeLine
        }

        private static void ApplyPeriodFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, DateTime startDate, DateTime endDate)
        {
            sqlParams.Add(new DynamicParam("StartDate", startDate));
            sqlParams.Add(new DynamicParam("EndDate", endDate));
            query.Replace("--PeriodFilter--", string.Empty); //TODO заменить на IncludeLine
        }

        private static void ApplyTopConstraint(this StringBuilder query, ICollection<DynamicParam> sqlParams, int count)
        {
            sqlParams.Add(new DynamicParam("TopConstraint", count));
            query.Replace("--TopConstraint--", string.Empty); //TODO заменить на IncludeLine
        }
    }
}
