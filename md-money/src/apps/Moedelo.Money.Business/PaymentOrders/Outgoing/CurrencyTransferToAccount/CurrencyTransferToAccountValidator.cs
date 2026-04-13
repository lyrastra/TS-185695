using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountValidator))]
    internal sealed class CurrencyTransferToAccountValidator : ICurrencyTransferToAccountValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public CurrencyTransferToAccountValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(CurrencyTransferToAccountSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            if (request.ToSettlementAccountId == null && request.OperationState != OperationState.MissingCurrencySettlementAccount)
            {
                throw new BusinessValidationException("ToSettlementAccountId", "Это поле должно быть заполнено.");
            }

            if (request.ToSettlementAccountId.HasValue)
            {
                await settlementAccountsValidator.ValidateCurrencyAccountsOfSameCurrencyTypeAsync(request.SettlementAccountId, request.ToSettlementAccountId.Value).ConfigureAwait(false);
                return;
            }
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId);
        }
    }
}