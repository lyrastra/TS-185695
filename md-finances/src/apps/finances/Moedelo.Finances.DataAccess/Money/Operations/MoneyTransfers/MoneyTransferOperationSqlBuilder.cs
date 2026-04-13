using System;
using System.Collections.Generic;
using System.Text;
using Moedelo.Finances.DataAccess.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.Scripts.MoneyTransfers
{
    internal static class MoneyTransferOperationSqlBuilder
    {
        internal static QueryObjectWithDynamicParams GetIncomingBalanceOperationQuery(int firmId, int settlementAccountId)
        {
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
            };
            var sql = new StringBuilder(MoneyTransferOperationQeries.GetByFirm);
            sql.ApplyTopConstraint(1);
            sql.ApplySettlementAccountFilter(sqlParams, settlementAccountId);
            sql.ApplyOperationTypeFilter(sqlParams, "CurrencyBalanceOperation");
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        internal static QueryObjectWithDynamicParams GetByBaseIdsQuery(int firmId, IReadOnlyCollection<long> baseIds)
        {
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
            };
            var sql = new StringBuilder(MoneyTransferOperationQeries.GetByFirm);
            sql.ApplyBaseIdsFilter(sqlParams, baseIds);
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        internal static QueryObjectWithDynamicParams GetForReconciliationQuery(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate)
        {
            var sqlParams = new List<DynamicParam>
            {
                new DynamicParam("FirmId", firmId),
            };
            var sql = new StringBuilder(MoneyTransferOperationQeries.GetByFirm);
            sql.ApplySettlementAccountFilter(sqlParams, settlementAccountId);
            sql.ApplyPeriodFilter(sqlParams, startDate, endDate);
            return new QueryObjectWithDynamicParams(sql.ToString(), sqlParams);
        }

        private static void ApplyBaseIdsFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, IReadOnlyCollection<long> baseIds)
        {
            sqlParams.Add(new DynamicParam("BaseIds", baseIds.ToBigIntListTVP()));
            query.AppendLine("and fo.DocumentBaseId in (select Id from @BaseIds)");
        }

        private static void ApplySettlementAccountFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, int settlementAccountId)
        {
            sqlParams.Add(new DynamicParam("SettlementAccountId", settlementAccountId));
            query.AppendLine("and (mto.SettlementAccountId = @SettlementAccountId or mto.MovementSettlementAccountId = @SettlementAccountId)");
        }

        private static void ApplyPeriodFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, DateTime startDate, DateTime endDate)
        {
            sqlParams.Add(new DynamicParam("StartDate", startDate));
            sqlParams.Add(new DynamicParam("EndDate", endDate));
            query.AppendLine("and fo.operation_date between @StartDate and @EndDate");
        }

        private static void ApplyOperationTypeFilter(this StringBuilder query, ICollection<DynamicParam> sqlParams, string operationType)
        {
            sqlParams.Add(new DynamicParam("OperationType", operationType));
            query.AppendLine("and fo.type = @OperationType");
        }

        private static void ApplyTopConstraint(this StringBuilder query, int count)
        {
            query.Replace("/* TopConstraint */", $"top ({count})");
        }
    }
}
