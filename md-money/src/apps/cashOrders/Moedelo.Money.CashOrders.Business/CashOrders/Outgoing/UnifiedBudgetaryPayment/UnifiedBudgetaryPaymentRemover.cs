using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentRemover))]
    class UnifiedBudgetaryPaymentRemover : IUnifiedBudgetaryPaymentRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashOrderRemover cashOrderRemover;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;

        public UnifiedBudgetaryPaymentRemover(
            IExecutionInfoContextAccessor contextAccessor,
            ICashOrderRemover cashOrderRemover,
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.cashOrderRemover = cashOrderRemover;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
        }

        public async Task<IReadOnlyCollection<long>> DeleteAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;

            await cashOrderRemover.DeleteAsync(documentBaseId);
            return await unifiedTaxPaymentDao.DeleteAsync((int)context.FirmId, documentBaseId);
        }
    }
}
