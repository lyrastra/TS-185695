using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moedelo.Common.Enums.Extensions.Finances.Money;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.Finances.Domain.Models.Money;

namespace Moedelo.Finances.DataAccess.Money.Balances
{
    public static class AccMoneyBalancesQueryBuilder
    {
        public static QueryObject GetBySourceIds(int firmId, BalancesRequest request)
        {
            var param = GetParams(firmId, request.BalancesMasterDate, request.OnDate);
            var sql = GetQueryBySourceIds(request.MoneySources);
            return new QueryObject(sql, param);
        }

        private static object GetParams(int firmId, DateTime balancesMasterDate, DateTime? onDate)
        {
            return new
            {
                firmId,
                balancesMasterDate,
                OnDate = onDate ?? DateTime.Today,
                IncomingDirection = MoneyDirection.Incoming,
                DefaultOperationState = OperationState.Default,
                BadOperationStates = OperationStateExtensions.BadOperationStates.Cast<int>().ToIntListTVP(),
                MoneySourceTypeSettlementAccount = MoneySourceType.SettlementAccount,
                MoneySourceTypeCash = MoneySourceType.Cash,
                MoneySourceTypePurse = MoneySourceType.Purse,
                PayedDocumentStatus = DocumentStatus.Payed
            };
        }

        private static string GetQuery()
        {
            return
                BalancesQueries.GetPaymentOrderBalances +
                Environment.NewLine + "union all" + Environment.NewLine +
                BalancesQueries.GetCashOrderBalances +
                Environment.NewLine + "union all" + Environment.NewLine +
                BalancesQueries.GetPurseOperationBalances;
        }

        private static string GetQueryBySourceIds(IReadOnlyCollection<MoneySourceBase> moneySources)
        {
            var result = new StringBuilder();
            var moneySourcesByTypes = moneySources.GroupBy(x => x.Type, x => x.Id);
            foreach (var moneySourcesByType in moneySourcesByTypes)
            {
                string query;
                string filter;
                switch (moneySourcesByType.Key)
                {
                    case MoneySourceType.SettlementAccount:
                        query = BalancesQueries.GetPaymentOrderBalances;
                        filter = $" and SettlementAccountId in ({string.Join(",", moneySourcesByType)})";
                        break;
                    case MoneySourceType.Cash:
                        query = BalancesQueries.GetCashOrderBalances;
                        filter = $" and CashId in ({string.Join(",", moneySourcesByType)})";
                        break;
                    case MoneySourceType.Purse:
                        query = BalancesQueries.GetPurseOperationBalances;
                        filter = $" and PurseId in ({string.Join(",", moneySourcesByType)})";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                query = query.Replace("/* SourceFilter */", filter);
                if (result.Length > 0)
                {
                    result.Append(" union all ");
                }
                result.Append(query);
            }
            return result.ToString();
        }
    }
}
