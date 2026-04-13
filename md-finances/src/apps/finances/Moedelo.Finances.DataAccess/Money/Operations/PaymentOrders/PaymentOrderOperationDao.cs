using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.DataAccess.Money.Operations.PaymentOrders
{
    [InjectAsSingleton]
    public class PaymentOrderOperationDao : IPaymentOrderOperationDao
    {
        private static readonly OperationState[] UnrecognizedOperationStateList = UnrecognizedOperationStates.List;

        private readonly IMoedeloDbExecutor dbExecutor;
        private readonly IMoedeloReadOnlyDbExecutor readOnlyDbExecutor;

        public PaymentOrderOperationDao(
            IMoedeloDbExecutor dbExecutor,
            IMoedeloReadOnlyDbExecutor readOnlyDbExecutor)
        {
            this.dbExecutor = dbExecutor;
            this.readOnlyDbExecutor = readOnlyDbExecutor;
        }

        public Task<List<long>> GetBaseIdsForUncategorizedAsync(int firmId, long? sourceId)
        {
            var param = new
            {
                firmId,
                RegularOperationState = OperationState.Default,
                UnrecognizedOperationStates = UnrecognizedOperationStateList.Cast<int>().ToIntListTVP(),
                sourceId,
                UnconfirmedOutsourceState = OutsourceState.Unconfirmed
            };
            var queryObject = new QueryObject(PaymentOrderOperationQueries.GetBaseIdsForUncategorized, param);
            return dbExecutor.QueryAsync<long>(queryObject);
        }

        public Task<List<PaymentOrderOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            var queryObject = PaymentOrderOperationSqlBuilder.GetByBaseIdsQuery(firmId, baseIds);
            return dbExecutor.QueryAsync<PaymentOrderOperation>(queryObject);
        }

        public Task<List<long>> GetDuplicateBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            var param = new
            {
                firmId,
                baseIds = baseIds.ToBigIntListTVP()
            };
            var queryObject = new QueryObject(PaymentOrderOperationQueries.GetDuplicateBaseIdsByOriginalBaseIds, param);
            return dbExecutor.QueryAsync<long>(queryObject);
        }

        public Task<List<PaymentOrderOperation>> GetForReconciliationAsync(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate)
        {
            var queryObject = PaymentOrderOperationSqlBuilder.GetForReconciliationQuery(firmId, settlementAccountId, startDate, endDate);
            return readOnlyDbExecutor.QueryAsync<PaymentOrderOperation>(queryObject);
        }

        public Task<DateTime?> GetLastDateUntilAsync(int firmId, DateTime date)
        {
            var queryObject = new QueryObject(PaymentOrderOperationQueries.GetLastDateUntil, new { firmId, date });
            return dbExecutor.FirstOrDefaultAsync<DateTime?>(queryObject);
        }

        public Task<List<PaymentOrderStatus>> GetStatusesByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentsBaseIdList)
        {
            var param = new
            {
                firmId,
                baseIds = documentsBaseIdList.ToBigIntListTVP()
            };
            var queryObject = new QueryObject(PaymentOrderOperationQueries.GetStatusesByBaseIds, param);
            return dbExecutor.QueryAsync<PaymentOrderStatus>(queryObject);
        }

        public async Task<PaymentOrderOperation> GetIncomingBalanceOperationAsync(int firmId, int settlementAccountId)
        {
            var queryObject = PaymentOrderOperationSqlBuilder.GetIncomingBalanceOperationQuery(firmId, settlementAccountId);
            var result = await dbExecutor.QueryAsync<PaymentOrderOperation>(queryObject).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public Task<bool> HasOperationsBySettlementAccountAsync(int firmId, int settlementAccountId)
        {
            var param = new { firmId, settlementAccountId };
            var queryObject = new QueryObject(PaymentOrderOperationQueries.HasOperationsBySettlementAccount, param);
            return dbExecutor.FirstOrDefaultAsync<bool>(queryObject);
        }

        public Task<List<PaymentOrderOperation>> GetFor1cConfirmationAsync(DateTime startDate, DateTime endDate)
        {
            var queryObject = PaymentOrderOperationSqlBuilder.GetFor1cConfirmationQuery(startDate, endDate);
            return dbExecutor.QueryAsync<PaymentOrderOperation>(queryObject);
        }

        public Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsAsync(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var queryObject = PaymentOrderOperationSqlBuilder.GetBudgetaryPaymentsQuery(firmId, queryParams);
            return readOnlyDbExecutor.QueryAsync<PaymentOrderOperation>(queryObject);
        }

        public Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(int firmId, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var queryObject = PaymentOrderOperationSqlBuilder.GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(firmId, queryParams);
            var resultMap = new Dictionary<long, PaymentOrderOperation>();
            return readOnlyDbExecutor.QueryDistinctAsync(
                queryObject,
                new[] { typeof(PaymentOrderOperation), typeof(UnifiedBudgetarySubPayment) },
                row =>
                {
                    var operation = (PaymentOrderOperation)row[0];
                    var subPayment = (UnifiedBudgetarySubPayment)row[1];

                    var exists = resultMap.TryGetValue(operation.DocumentBaseId, out var existingOperation);
                    if (!exists)
                    {
                        existingOperation = operation;
                        resultMap[operation.DocumentBaseId] = existingOperation;
                    }

                    if (subPayment?.DocumentBaseId > 0)
                    {
                        existingOperation.SubPayments.Add(subPayment);
                    }

                    return existingOperation;
                },
                splitOn: "ParentDocumentId");
        }
    }
}