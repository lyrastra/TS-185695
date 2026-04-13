using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentRemover))]
    internal class UnifiedBudgetaryPaymentRemover : IUnifiedBudgetaryPaymentRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;
        private readonly IPaymentOrderRemover paymentOrderRemover;

        public UnifiedBudgetaryPaymentRemover(
            IExecutionInfoContextAccessor contextAccessor, 
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao, 
            IPaymentOrderRemover paymentOrderRemover)
        {
            this.contextAccessor = contextAccessor;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
            this.paymentOrderRemover = paymentOrderRemover;
        }
        
        public async Task<IReadOnlyList<long>> DeleteAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;

            await paymentOrderRemover.DeleteAsync(documentBaseId);
            return await unifiedTaxPaymentDao.DeleteAsync((int)context.FirmId, documentBaseId);
        }
    }
}