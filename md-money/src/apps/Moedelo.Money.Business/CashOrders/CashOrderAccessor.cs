using System.Linq;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders;

namespace Moedelo.Money.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderAccessor))]
    internal class CashOrderAccessor : ICashOrderAccessor
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public CashOrderAccessor(IExecutionInfoContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public bool IsReadOnly(bool provideInAccounting)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var isUsnAccountantTariff = context.UserRules.Contains(AccessRule.UsnAccountantTariff);
            var canViewPostings = context.UserRules.Contains(AccessRule.ViewPostings);

            return provideInAccounting 
                && isUsnAccountantTariff
                && canViewPostings == false;
        }
    }
}
