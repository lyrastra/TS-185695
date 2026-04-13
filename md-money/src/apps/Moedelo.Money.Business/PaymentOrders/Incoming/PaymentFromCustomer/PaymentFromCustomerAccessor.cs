using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerAccessor))]
    internal sealed class PaymentFromCustomerAccessor : IPaymentFromCustomerAccessor
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public PaymentFromCustomerAccessor(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public bool IsReadOnly(PaymentFromCustomerResponse payment)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var isReadOnlyCommon = paymentOrderAccessor.IsReadOnly(payment);
            var editClientPayments = context.HasAnyRule(AccessRule.AccessEditClientPayments);

            return isReadOnlyCommon
                || editClientPayments == false;
        }
    }
}
