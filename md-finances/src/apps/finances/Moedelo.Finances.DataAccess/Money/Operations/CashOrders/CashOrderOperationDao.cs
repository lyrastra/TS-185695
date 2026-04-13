using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.CashOrders
{
    [InjectAsSingleton]
    public class CashOrderOperationDao : ICashOrderOperationDao
    {
        private readonly IMoedeloReadOnlyDbExecutor readOnlyDbExecutor;

        public CashOrderOperationDao(IMoedeloReadOnlyDbExecutor readOnlyDbExecutor)
        {
            this.readOnlyDbExecutor = readOnlyDbExecutor;
        }

        public Task<List<CashOrderOperation>> GetBudgetaryPaymentsAsync(int firmId, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            var queryObject = QueryBuilder.GetBudgetaryPaymentsByQueryParamsQuery(firmId, queryParams);
            return readOnlyDbExecutor.QueryAsync<CashOrderOperation>(queryObject);
        }

        public Task<List<CashOrderOperation>> GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(int firmId, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            var queryObject = QueryBuilder.GetBudgetaryPaymentsWithUnifiedTaxPayments(firmId, queryParams);
            var resultMap = new Dictionary<long, CashOrderOperation>();
            return readOnlyDbExecutor.QueryDistinctAsync(
                queryObject,
                new[] { typeof(CashOrderOperation), typeof(UnifiedBudgetarySubPayment) },
                row =>
                {
                    var operation = (CashOrderOperation)row[0];
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