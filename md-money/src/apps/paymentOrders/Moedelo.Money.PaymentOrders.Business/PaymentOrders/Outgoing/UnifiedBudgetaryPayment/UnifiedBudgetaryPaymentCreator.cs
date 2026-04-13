using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentCreator))]
    internal class UnifiedBudgetaryPaymentCreator: IUnifiedBudgetaryPaymentCreator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;
        private readonly IPaymentOrderCreator paymentOrderCreator;
        private readonly UnifiedBudgetaryPaymentHelper helper;

        public UnifiedBudgetaryPaymentCreator(
            IExecutionInfoContextAccessor contextAccessor, 
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao, 
            IPaymentOrderCreator paymentOrderCreator,
            UnifiedBudgetaryPaymentHelper helper)
        {
            this.contextAccessor = contextAccessor;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
            this.paymentOrderCreator = paymentOrderCreator;
            this.helper = helper;
        }

        public async Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            await helper.FillRequestAsync(request);
            var response = await paymentOrderCreator.CreateAsync(request);
            await unifiedTaxPaymentDao.InsertAsync((int)context.FirmId, request.DocumentBaseId, request.UnifiedBudgetarySubPayments);
            return response;
        }
    }
}