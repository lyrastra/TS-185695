using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentUpdater))]
    class UnifiedBudgetaryPaymentUpdater : IUnifiedBudgetaryPaymentUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashOrderUpdater cashOrderUpdater;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;

        public UnifiedBudgetaryPaymentUpdater(
            IExecutionInfoContextAccessor contextAccessor,
            ICashOrderUpdater cashOrderUpdater,
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.cashOrderUpdater = cashOrderUpdater;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
        }

        public async Task<IReadOnlyCollection<long>> UpdateAsync(CashOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            await cashOrderUpdater.UpdateAsync(request);
            return await unifiedTaxPaymentDao.OverwriteAsync((int)context.FirmId, request.DocumentBaseId, request.UnifiedBudgetarySubPayments);
        }
    }
}
