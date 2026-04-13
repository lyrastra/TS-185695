using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.Validation.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee
{
    [InjectAsSingleton(typeof(IBankFeeValidator))]
    internal sealed class BankFeeValidator : IBankFeeValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public BankFeeValidator(
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

        public async Task ValidateAsync(BankFeeSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);

            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value).ConfigureAwait(false);
            }

            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);

            if (request.TaxPostings.ProvidePostingType == Enums.ProvidePostingType.ByHand)
            {
                if (request.TaxPostings?.UsnTaxPostings.Sum(x => x.Sum) > request.Sum)
                {
                    throw new BusinessValidationException("TaxPostings.UsnTaxPostings", "Сумма проводок не может быть больше общей суммы операции");
                }
                if (request.TaxPostings?.OsnoTaxPostings.Sum(x => x.Sum) > request.Sum)
                {
                    throw new BusinessValidationException("TaxPostings.OsnoTaxPostings", "Сумма проводок не может быть больше общей суммы операции");
                }
            }
        }
    }
}