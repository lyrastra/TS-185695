using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(CurrencyTransferFromAccountImportValidator))]
    internal sealed class CurrencyTransferFromAccountImportValidator
    {
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public CurrencyTransferFromAccountImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(CurrencyTransferFromAccountImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
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
