using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(RetailRevenueImportValidator))]
    internal sealed class RetailRevenueImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;

        public RetailRevenueImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
        }

        public async Task ValidateAsync(RetailRevenueImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value).ConfigureAwait(false);
            }
        }
    }
}