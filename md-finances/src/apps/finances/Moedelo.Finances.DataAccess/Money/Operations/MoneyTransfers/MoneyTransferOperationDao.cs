using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Finances.DataAccess.Money.Operations.Scripts.MoneyTransfers;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Kontragents;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.MoneyTransfers
{
    [InjectAsSingleton]
    public class MoneyTransferOperationDao : IMoneyTransferOperationDao
    {
        private readonly IMoedeloDbExecutor dbExecutor;
        private readonly IMoedeloReadOnlyDbExecutor readOnlyDbExecutor;

        public MoneyTransferOperationDao(
            IMoedeloDbExecutor dbExecutor,
            IMoedeloReadOnlyDbExecutor readOnlyDbExecutor)
        {
            this.dbExecutor = dbExecutor;
            this.readOnlyDbExecutor = readOnlyDbExecutor;
        }

        public Task<List<MoneyTransferOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            var queryObject = MoneyTransferOperationSqlBuilder.GetByBaseIdsQuery(firmId, baseIds);
            return readOnlyDbExecutor.QueryAsync<MoneyTransferOperation>(queryObject);
        }

        public Task<List<KontragentTurnover>> TopByOperationsWithKontragentsAsync(int firmId, int count, DateTime startDate, DateTime endDate)
        {
            var param = new { firmId, count, startDate, endDate };
            var queryObject = new QueryObject(MoneyTransferOperationQeries.TopByOperationsWithKontragents, param);
            return readOnlyDbExecutor.QueryAsync<KontragentTurnover>(queryObject);
        }

        public Task<List<MoneyTransferOperation>> GetForReconciliationAsync(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate)
        {
            var queryObject = MoneyTransferOperationSqlBuilder.GetForReconciliationQuery(firmId, settlementAccountId, startDate, endDate);
            return readOnlyDbExecutor.QueryAsync<MoneyTransferOperation>(queryObject);
        }

        public async Task<MoneyTransferOperation> GetIncomingBalanceOperationAsync(int firmId, int settlementAccountId)
        {
            var queryObject = MoneyTransferOperationSqlBuilder.GetIncomingBalanceOperationQuery(firmId, settlementAccountId);
            var result = await readOnlyDbExecutor.QueryAsync<MoneyTransferOperation>(queryObject).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public Task<bool> HasOperationsBySettlementAccountAsync(int firmId, int settlementAccountId)
        {
            var param = new { firmId, settlementAccountId };
            var queryObject = new QueryObject(MoneyTransferOperationQeries.HasOperationsBySettlementAccount, param);
            return dbExecutor.FirstOrDefaultAsync<bool>(queryObject);
        }

        public Task<int> InsertAsync(int firmId, MoneyTransferOperation operation)
        {
            var param = new
            {
                firmId,
                operation.OperationType,
                operation.Number,
                operation.Date,
                operation.MoneyBayType,
                operation.DocumentBaseId,
                operation.SettlementAccountId,
                operation.Sum
            };
            var queryObject = new QueryObject(MoneyTransferOperationQeries.Insert, param);
            return dbExecutor.FirstOrDefaultAsync<int>(queryObject);
        }

        public Task UpdateAsync(int firmId, MoneyTransferOperation operation)
        {
            var param = new
            {
                operation.Id,
                firmId,
                operation.OperationType,
                operation.Number,
                operation.Date,
                operation.MoneyBayType,
                operation.DocumentBaseId,
                operation.SettlementAccountId,
                operation.Sum
            };
            var queryObject = new QueryObject(MoneyTransferOperationQeries.Update, param);
            return dbExecutor.ExecuteAsync(queryObject);
        }

        public Task DeleteByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            var param = new { firmId, baseIds = baseIds.ToBigIntListTVP() };
            var queryObject = new QueryObject(MoneyTransferOperationQeries.DeleteByBaseIds, param);
            return dbExecutor.ExecuteAsync(queryObject);
        }

        public async Task<Dictionary<int, bool>> GetIsBalanceSettedForFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            var result = await readOnlyDbExecutor.QueryAsync<KeyValuePair<int, bool>, FirmIdRow>("FirmIds",
                firmIds.Select(x => new FirmIdRow {FirmId = x}).ToList(),
                new QueryObject(MoneyTransferOperationQeries.GetIsBalanceSettedForFirms));
            
            return result.ToDictionary(k => k.Key, v => v.Value);
        }

        private class FirmIdRow
        {
            public int FirmId { get; set; }
        }
    }
}