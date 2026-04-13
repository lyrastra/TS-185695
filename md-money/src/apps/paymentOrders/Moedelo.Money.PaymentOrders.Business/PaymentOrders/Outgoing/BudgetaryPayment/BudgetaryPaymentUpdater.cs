using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentUpdater))]
    internal class BudgetaryPaymentUpdater : IBudgetaryPaymentUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderUpdater paymentOrderUpdater;
        private readonly IBudgetaryPaymentDao budgetaryPaymentDao;

        public BudgetaryPaymentUpdater(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderUpdater paymentOrderUpdater,
            IBudgetaryPaymentDao budgetaryPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderUpdater = paymentOrderUpdater;
            this.budgetaryPaymentDao = budgetaryPaymentDao;
        }

        public async Task UpdateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            request.BudgetaryFields.PayerKpp = await budgetaryPaymentDao.GetPayerKppAsync((int)context.FirmId, request.DocumentBaseId);
            await paymentOrderUpdater.UpdateAsync(request).ConfigureAwait(false);
        }
    }
}
