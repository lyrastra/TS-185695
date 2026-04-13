using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderAccessor))]
    internal sealed class PaymentOrderAccessor : IPaymentOrderAccessor
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public PaymentOrderAccessor(IExecutionInfoContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Возвращает признак "Только чтение" на основании признака "Проведен бухгалтером" и прав доступа
        /// </summary>
        public bool IsProvidedReadOnly(bool provideInAccounting)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var hasRules = context.HasAllRules(AccessRule.UsnAccountantTariff, AccessRule.ViewPostings);
            return provideInAccounting && hasRules == false;
        }

        public bool IsReadOnly(IAccessorPropsResponse payment)
        {
            if (payment == null)
            {
                return false;
            }

            return IsProvidedReadOnly(payment.ProvideInAccounting)
                   || payment.OutsourceState == OutsourceState.Unconfirmed;
        }
    }
}
