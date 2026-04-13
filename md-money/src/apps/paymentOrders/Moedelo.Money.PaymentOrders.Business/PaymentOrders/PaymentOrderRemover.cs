using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Extensions;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderRemover))]
    internal class PaymentOrderRemover : IPaymentOrderRemover
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPaymentOrderDao dao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public PaymentOrderRemover(
            IExecutionInfoContextAccessor executionInfoContext,
            IPaymentOrderDao dao,
            IHistoricalLogsCommandWriter historicalLogsWriter)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task DeleteAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var paymentOrder = await dao.GetAsync((int)context.FirmId, documentBaseId);
            paymentOrder.CheckExistence(documentBaseId);
            await dao.DeleteAsync((int)context.FirmId, paymentOrder.DocumentBaseId);
            await historicalLogsWriter.WriteAsync(LogOperationType.Delete, paymentOrder);
        }

        public async Task<long[]> DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null && documentBaseIds.Count <= 0)
            {
                return Array.Empty<long>();
            }
            var context = executionInfoContext.ExecutionInfoContext;
            var deletedBaseIds = await dao.DeleteInvalidAsync((int)context.FirmId, documentBaseIds);
            return deletedBaseIds.ToArray();
        }
    }
}
