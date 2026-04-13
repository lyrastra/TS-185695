using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Extensions;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderReader))]
    internal class PaymentOrderReader : IPaymentOrderReader
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPaymentOrderDao dao;
        private readonly IPaymentOrderReadOnlyDao readOnlyDao;

        public PaymentOrderReader(
            IExecutionInfoContextAccessor executionInfoContext,
            IPaymentOrderDao dao,
            IPaymentOrderReadOnlyDao readOnlyDao)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
            this.readOnlyDao = readOnlyDao;
        }

        public async Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId, OperationType operationType)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await dao.GetAsync((int)context.FirmId, documentBaseId);
            CheckPaymentOrder(documentBaseId, operationType, paymentOrder);
            var paymentOrderSnapshot = paymentOrder.ToSnapshot();
            return new PaymentOrderResponse
            {
                PaymentOrder = paymentOrder,
                PaymentOrderSnapshot = paymentOrderSnapshot
            };
        }

        public async Task<IReadOnlyCollection<PaymentOrderResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds, OperationType operationType)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrders = await dao.GetByBaseIdsAsync((int)context.FirmId, documentBaseIds, operationType);
            return paymentOrders.Select(paymentOrder =>
                new PaymentOrderResponse
                {
                    PaymentOrder = paymentOrder,
                    PaymentOrderSnapshot = paymentOrder.ToSnapshot()
                })
                .ToArray();
        }

        public async Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var baseIds = await dao.GetBaseIdsByOperationTypeAsync((int)context.FirmId, operationType, year);
            return baseIds ?? new List<long>();
        }

        public async Task<long> GetOperationIdAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await dao.GetAsync((int)context.FirmId, documentBaseId);
            if (paymentOrder == null)
            {
                throw new PaymentOrderNotFoundExcepton(documentBaseId);
            }
            return paymentOrder.Id;
        }

        public async Task<PaymentOrderDuplicateDataResponse> GetDuplicateDataAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await dao.GetAsync((int)context.FirmId, documentBaseId);
            if (paymentOrder == null)
            {
                throw new PaymentOrderNotFoundExcepton(documentBaseId);
            }
            return new PaymentOrderDuplicateDataResponse
            {
                Date = paymentOrder.Date,
                OperationType = paymentOrder.OperationType,
                DuplicateId = paymentOrder.DuplicateId,
                OperationState = paymentOrder.OperationState
            };
        }

        public async Task<OperationType> GetOperationTypeAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await dao.GetAsync((int)context.FirmId, documentBaseId);
            if (paymentOrder == null)
            {
                throw new PaymentOrderNotFoundExcepton(documentBaseId);
            }
            return paymentOrder.OperationType;
        }

        public Task<bool> GetIsFromImportAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            return dao.GetIsFromImportAsync((int)context.FirmId, documentBaseId);
        }

        private static void CheckPaymentOrder(long documentBaseId, OperationType operationType, PaymentOrder paymentOrder)
        {
            paymentOrder.CheckExistence(documentBaseId);
            paymentOrder.CheckType(operationType);
        }

        public async Task<long> GetBaseIdByIdAsync(long id)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var baseId = await dao.GetBaseIdByIdAsync((int)context.FirmId, id);
            if (baseId == null)
            {
                throw new PaymentOrderNotFoundExcepton(0); // ???
            }
            return baseId.Value;
        }

        public Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return dao.GetOperationTypeByBaseIdsAsync((int)context.FirmId, documentBaseIds);
        }

        public Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return readOnlyDao.GetOutgoingNumbersAsync((int)context.FirmId, settlementAccountId, year, cut);
        }

        public Task<bool> GetIsPaidAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return readOnlyDao.GetIsPaidAsync((int)context.FirmId, documentBaseId);
        }

        public Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusAsync(DocumentsStatusRequest request)
        {
            return readOnlyDao.GetDocumentsStatusAsync(request);
        }

        public async Task<PaymentOrderSnapshot> GetPaymentSnapshotAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await dao.GetAsync((int)context.FirmId, documentBaseId);
            return paymentOrder?.ToSnapshot();
        }
    }
}