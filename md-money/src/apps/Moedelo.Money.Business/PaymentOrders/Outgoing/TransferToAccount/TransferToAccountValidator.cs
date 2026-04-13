using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Exceptions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountValidator))]
    internal sealed class TransferToAccountValidator : ITransferToAccountValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public TransferToAccountValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator, 
            NumberValidator numberValidator, 
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        public async Task ValidateAsync(TransferToAccountSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);

            EnsureSettlementAccount(request.ToSettlementAccountId, nameof(request.ToSettlementAccountId));
            await settlementAccountsValidator.ValidateAsync(request.ToSettlementAccountId.GetValueOrDefault()).ConfigureAwait(false);
        }

        private async Task ValidatePaymentNumber(TransferToAccountSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number).ConfigureAwait(false);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId).ConfigureAwait(false);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number)
                    .ConfigureAwait(false);
            }
        }

        private static void EnsureSettlementAccount(int? settlementAccountId, string paramName)
        {
            if (settlementAccountId is null or 0)
            {
                throw new BusinessValidationException(paramName, "Это поле должно быть заполнено.");
            }
        }
    }
}