using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferFromAccountValidator))]
    internal sealed class CurrencyTransferFromAccountValidator : ICurrencyTransferFromAccountValidator
    {
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public CurrencyTransferFromAccountValidator(
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

        public async Task ValidateAsync(CurrencyTransferFromAccountSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            if (request.FromSettlementAccountId == null && request.OperationState != OperationState.MissingCurrencySettlementAccount)
            {
                throw new BusinessValidationException("ToSettlementAccountId", "Это поле должно быть заполнено.");
            }

            if (request.FromSettlementAccountId.HasValue)
            {
                await settlementAccountsValidator.ValidateCurrencyAccountsOfSameCurrencyTypeAsync(request.SettlementAccountId, request.FromSettlementAccountId.Value).ConfigureAwait(false);
                return;
            }
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId);
        }
    }
}
