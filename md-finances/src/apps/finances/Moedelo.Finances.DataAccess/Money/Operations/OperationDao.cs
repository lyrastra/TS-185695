using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Domain.Models.Kontragents;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations
{
    [InjectAsSingleton]
    public class OperationDao : IOperationDao
    {
        private readonly IMoedeloDbExecutor dbExecutor;

        public OperationDao(IMoedeloDbExecutor dbExecutor)
        {
            this.dbExecutor = dbExecutor;
        }

        public Task<MoneyOperation> GetByBaseIdAsync(int firmId, long documentBaseId)
        {
            var param = new
            {
                firmId,
                documentBaseId,
                CurrencyRub = Currency.RUB,
                PayedDocumentStatus = DocumentStatus.Payed,
                RegularOperationState = OperationState.Default,
                PaymentOrderOperationKind = OperationKind.PaymentOrderOperation,
                CashOrderOperationKind = OperationKind.CashOrderOperation,
                PurseOperationOperationKind = OperationKind.PurseOperation,
            };
            var sql = BuildSql.From(OperationQueries.GetOperations).IncludeLine("FilterByBaseId").ToString();
            var queryObject = new QueryObject(sql, param);
            return dbExecutor.FirstOrDefaultAsync<MoneyOperation>(queryObject);
        }

        public async Task<MoneyOperation[]> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            var param = new
            {
                firmId,
                BaseIds = baseIds.ToBigIntListTVP(),
                CurrencyRub = Currency.RUB,
                PayedDocumentStatus = DocumentStatus.Payed,
                RegularOperationState = OperationState.Default,
                PaymentOrderOperationKind = OperationKind.PaymentOrderOperation,
                CashOrderOperationKind = OperationKind.CashOrderOperation,
                PurseOperationOperationKind = OperationKind.PurseOperation,
            };
            var sql = BuildSql.From(OperationQueries.GetOperations).IncludeLine("FilterByBaseIds").ToString();
            var queryObject = new QueryObject(sql, param);

            return (await dbExecutor.QueryAsync<MoneyOperation>(queryObject).ConfigureAwait(false)).ToArray();
        }

        public async Task<MoneyOperation[]> GetByPeriodAsync(int firmId, Period period)
        {
            var param = new
            {
                firmId,
                period.StartDate,
                period.EndDate,
                CurrencyRub = Currency.RUB,
                PayedDocumentStatus = DocumentStatus.Payed,
                RegularOperationState = OperationState.Default,
                PaymentOrderOperationKind = OperationKind.PaymentOrderOperation,
                CashOrderOperationKind = OperationKind.CashOrderOperation,
                PurseOperationOperationKind = OperationKind.PurseOperation,
            };
            var sql = BuildSql.From(OperationQueries.GetOperations).IncludeLine("FilterByPeriod").ToString();
            var queryObject = new QueryObject(sql, param);
            return (await dbExecutor.QueryAsync<MoneyOperation>(queryObject).ConfigureAwait(false)).ToArray();
        }

        public Task<List<OperationKindAndBaseId>> GetOperationsKindsByIdsAsync(int firmId, IReadOnlyCollection<long> ids)
        {
            var param = new
            {
                firmId,
                Ids = ids.ToBigIntListTVP(),
                PaymentOrderOperationKind = OperationKind.PaymentOrderOperation,
                CashOrderOperationKind = OperationKind.CashOrderOperation,
                PurseOperationOperationKind = OperationKind.PurseOperation,
            };
            var queryObject = new QueryObject(OperationQueries.GetOperationsKindsByIds, param);
            return dbExecutor.QueryAsync<OperationKindAndBaseId>(queryObject);
        }

        public Task<OperationKind?> GetKindByBaseIdAsync(int firmId, long documentBaseId)
        {
            var queryObject = new QueryObject(OperationQueries.GetOperationKind, new { firmId, documentBaseId });
            return dbExecutor.FirstOrDefaultAsync<OperationKind?>(queryObject);
        }

        public Task<List<OperationKindAndBaseId>> GetKindsByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds)
        {
            var param = new
            {
                firmId,
                BaseIds = documentBaseIds.ToBigIntListTVP(),
                PaymentOrderOperationKind = OperationKind.PaymentOrderOperation,
                CashOrderOperationKind = OperationKind.CashOrderOperation,
                PurseOperationOperationKind = OperationKind.PurseOperation,
            };
            var queryObject = new QueryObject(OperationQueries.GetOperationsKindsByBaseIds, param);
            return dbExecutor.QueryAsync<OperationKindAndBaseId>(queryObject);
        }

        public Task<List<KontragentTurnover>> TopByOperationsWithKontragentsAsync(int firmId, int count, DateTime startDate, DateTime endDate)
        {
            var param = new { firmId, count, startDate, endDate };
            var queryObject = new QueryObject(OperationQueries.TopByOperationsWithKontragentsAcc, param);
            return dbExecutor.QueryAsync<KontragentTurnover>(queryObject);
        }

        public async Task<MoneyOperation[]> GetByPatentAsync(int firmId, long patentId)
        {
            var param = new
            {
                FirmId = firmId,
                PatentId = patentId,
                CurrencyRub = Currency.RUB,
                PayedDocumentStatus = DocumentStatus.Payed,
                RegularOperationState = OperationState.Default,
                PaymentOrderOperationKind = OperationKind.PaymentOrderOperation,
                CashOrderOperationKind = OperationKind.CashOrderOperation,
                PurseOperationOperationKind = OperationKind.PurseOperation
            };
            var queryObject = new QueryObject(OperationQueries.GetByPatent, param);
            return (await dbExecutor.QueryAsync<MoneyOperation>(queryObject).ConfigureAwait(false)).ToArray();
        }
    }
}
