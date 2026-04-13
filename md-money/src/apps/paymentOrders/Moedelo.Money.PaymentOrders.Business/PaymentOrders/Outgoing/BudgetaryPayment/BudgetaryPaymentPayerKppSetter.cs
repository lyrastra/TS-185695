using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentPayerKppSetter))]
    internal class BudgetaryPaymentPayerKppSetter : IBudgetaryPaymentPayerKppSetter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBudgetaryPaymentDao budgetaryPaymentDao;
        private readonly IPaymentOrderReader paymentOrderReader;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public BudgetaryPaymentPayerKppSetter(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderReader paymentOrderReader,
            IHistoricalLogsCommandWriter historicalLogsWriter,
            IBudgetaryPaymentDao budgetaryPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.budgetaryPaymentDao = budgetaryPaymentDao;
            this.paymentOrderReader = paymentOrderReader;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task SetPayerKppAsync(long documentBaseId, string kpp)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await paymentOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.BudgetaryPayment);
            await budgetaryPaymentDao.SetPayerKppAsync((int)context.FirmId, response.PaymentOrder.DocumentBaseId, kpp);
            //todo: rework someday
            response = await paymentOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.BudgetaryPayment);
            await historicalLogsWriter.WriteAsync(LogOperationType.Update, response.PaymentOrder);
        }
    }
}
