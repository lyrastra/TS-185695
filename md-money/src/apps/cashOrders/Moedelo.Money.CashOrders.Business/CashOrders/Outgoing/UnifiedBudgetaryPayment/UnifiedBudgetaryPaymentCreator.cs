using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Common.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentCreator))]
    class UnifiedBudgetaryPaymentCreator : IUnifiedBudgetaryPaymentCreator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashOrderCreator cashOrderCreator;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;

        public UnifiedBudgetaryPaymentCreator(
            IExecutionInfoContextAccessor contextAccessor,
            ICashOrderCreator cashOrderCreator,
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.cashOrderCreator = cashOrderCreator;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
        }

        public async Task<CreateResponse> CreateAsync(CashOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;

            //var Kbktask = kbkReader.GetByIdAsync(request.CashOrder.KbkId.Value);

            var response = await cashOrderCreator.CreateAsync(request);
            await unifiedTaxPaymentDao.InsertAsync((int)context.FirmId, request.DocumentBaseId, request.UnifiedBudgetarySubPayments);
            return response;
        }
    }
}
