using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderImportedApprover))]
    internal class PaymentOrderImportedApprover : IPaymentOrderImportedApprover
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPaymentOrderDao dao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public PaymentOrderImportedApprover(
            IExecutionInfoContextAccessor executionInfoContext,
            IPaymentOrderDao dao,
            IHistoricalLogsCommandWriter historicalLogsWriter)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task ApproveAsync(int? settlementAccountId, DateTime? skipline)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var approvedBaseIds = await dao.ApproveImportedAsync((int)context.FirmId, settlementAccountId, skipline);
            await historicalLogsWriter.WriteApprovedAsync(approvedBaseIds);
        }
    }
}
