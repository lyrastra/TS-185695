using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.DataAccess.Abstractions;
using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;

namespace Moedelo.Money.CashOrders.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderReader))]
    internal class CashOrderReader : ICashOrderReader
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ICashOrderDao cashOrderDao;
        private readonly ICashOrderReadOnlyDao cashOrderReadOnlyDao;

        public CashOrderReader(
            IExecutionInfoContextAccessor executionInfoContext,
            ICashOrderDao cashOrderDao, 
            ICashOrderReadOnlyDao cashOrderReadOnlyDao)
        {
            this.executionInfoContext = executionInfoContext;
            this.cashOrderDao = cashOrderDao;
            this.cashOrderReadOnlyDao = cashOrderReadOnlyDao;
        }

        public async Task<CashOrderResponse> GetByBaseIdAsync(long documentBaseId, OperationType operationType)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var cashOrder = await cashOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            CheckCashOrder(documentBaseId, operationType, cashOrder);

            return new CashOrderResponse
            {
                CashOrder = cashOrder,
            };
        }

        public async Task<OperationType> GetOperationTypeAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var cashOrder = await cashOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            if (cashOrder == null)
            {
                throw new CashOrderNotFoundExcepton(documentBaseId);
            }
            return cashOrder.OperationType;
        }

        public Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return cashOrderDao.GetOperationTypeByBaseIdsAsync((int)context.FirmId, documentBaseIds);
        }

        public async Task<long> GetOperationIdAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var cashOrder = await cashOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            if (cashOrder == null)
            {
                throw new CashOrderNotFoundExcepton(documentBaseId);
            }
            return cashOrder.Id;
        }

        private static void CheckCashOrder(long documentBaseId, OperationType operationType, CashOrder cashOrder)
        {
            if (cashOrder == null)
            {
                throw new CashOrderNotFoundExcepton(documentBaseId);
            }
            if (cashOrder.OperationType == OperationType.BudgetaryPayment ||
               cashOrder.OperationType == OperationType.CashOrderOutgoingUnifiedBudgetaryPayment &&
               operationType == OperationType.BudgetaryPayment ||
               operationType == OperationType.CashOrderOutgoingUnifiedBudgetaryPayment)
            {
                // Для БП и ЕНП разрешаем менять типы друг на друга
                return;
            }
            if (cashOrder.OperationType != operationType)
            {
                throw new CashOrderMismatchTypeExcepton
                {
                    DocumentBaseId = documentBaseId,
                    Expected = operationType,
                    Actual = cashOrder.OperationType
                };
            }
        }

        public Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            return cashOrderReadOnlyDao.GetDocumentsStatusByBaseIdsAsync(documentBaseIds);
        }
    }
}