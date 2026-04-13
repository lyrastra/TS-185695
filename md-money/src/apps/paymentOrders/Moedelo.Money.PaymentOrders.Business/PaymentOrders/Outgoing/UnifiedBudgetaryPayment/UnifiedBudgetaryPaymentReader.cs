using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentReader))]
    internal class UnifiedBudgetaryPaymentReader: IUnifiedBudgetaryPaymentReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;
        private readonly IPaymentOrderReader paymentOrderReader;

        public UnifiedBudgetaryPaymentReader(
            IExecutionInfoContextAccessor contextAccessor, 
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao, 
            IPaymentOrderReader paymentOrderReader)
        {
            this.contextAccessor = contextAccessor;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
            this.paymentOrderReader = paymentOrderReader;
        }

        public async Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await paymentOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment);
            response.UnifiedBudgetarySubPayments = await unifiedTaxPaymentDao.GetByParentBaseIdAsync((int)context.FirmId, documentBaseId);
            return response;
        }
    }
}