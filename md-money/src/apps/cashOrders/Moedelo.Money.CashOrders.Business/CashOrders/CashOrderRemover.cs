using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.CashOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.CashOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderRemover))]
    internal class CashOrderRemover : ICashOrderRemover
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ICashOrderDao cashOrderDao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public CashOrderRemover(
            IExecutionInfoContextAccessor executionInfoContext,
            ICashOrderDao cashOrderDao,
            IHistoricalLogsCommandWriter historicalLogsWriter)
        {
            this.executionInfoContext = executionInfoContext;
            this.cashOrderDao = cashOrderDao;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task DeleteAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var cashOrder = await cashOrderDao.GetAsync((int)context.FirmId, documentBaseId);
            if (cashOrder == null)
            {
                throw new CashOrderNotFoundExcepton(documentBaseId);
            }

            await cashOrderDao.DeleteAsync((int)context.FirmId, cashOrder.DocumentBaseId);

            await historicalLogsWriter.WriteAsync(LogOperationType.Delete, cashOrder);
        }
    }
}
