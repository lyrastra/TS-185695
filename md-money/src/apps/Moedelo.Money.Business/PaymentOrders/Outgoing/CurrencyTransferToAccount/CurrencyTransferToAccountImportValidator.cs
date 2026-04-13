using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(CurrencyTransferToAccountImportValidator))]
    internal sealed class CurrencyTransferToAccountImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public CurrencyTransferToAccountImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(CurrencyTransferToAccountImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
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