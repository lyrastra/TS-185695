using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using System.Threading.Tasks;
using Moedelo.Money.Business.Validation.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestValidator))]
    class AccrualOfInterestValidator : IAccrualOfInterestValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public AccrualOfInterestValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator,
            TaxPostingsValidator taxPostingsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.taxPostingsValidator = taxPostingsValidator;
        }

        public async Task ValidateAsync(AccrualOfInterestSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value).ConfigureAwait(false);
            }

            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }
    }
}
