using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueValidator))]
    internal class RetailRevenueValidator : IRetailRevenueValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;

        public RetailRevenueValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ITaxationSystemValidator taxationSystemValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.taxationSystemValidator = taxationSystemValidator;
        }

        public async Task ValidateAsync(RetailRevenueSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value).ConfigureAwait(false);
            }
        }
    }
}