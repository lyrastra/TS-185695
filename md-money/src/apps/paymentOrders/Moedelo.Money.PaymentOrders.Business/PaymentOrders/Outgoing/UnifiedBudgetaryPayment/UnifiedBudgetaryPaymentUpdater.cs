using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentUpdater))]
    internal class UnifiedBudgetaryPaymentUpdater: IUnifiedBudgetaryPaymentUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderUpdater paymentOrderUpdater;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;
        private readonly UnifiedBudgetaryPaymentHelper helper;

        public UnifiedBudgetaryPaymentUpdater(
            IExecutionInfoContextAccessor contextAccessor, 
            IPaymentOrderUpdater paymentOrderUpdater, 
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao, 
            UnifiedBudgetaryPaymentHelper helper)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderUpdater = paymentOrderUpdater;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
            this.helper = helper;
        }

        public async Task<IReadOnlyCollection<long>> UpdateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            await helper.FillRequestAsync(request);
            await paymentOrderUpdater.UpdateAsync(request);
            return await unifiedTaxPaymentDao.OverwriteAsync((int)context.FirmId, request.DocumentBaseId, request.UnifiedBudgetarySubPayments);
        }
    }
}