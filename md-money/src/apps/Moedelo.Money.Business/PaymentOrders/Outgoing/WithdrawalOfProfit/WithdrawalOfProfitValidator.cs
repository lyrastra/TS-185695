using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.Enums;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(IWithdrawalOfProfitValidator))]
    internal sealed class WithdrawalOfProfitValidator : IWithdrawalOfProfitValidator
    {
        private readonly ILegalTypeValidator legalTypeValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public WithdrawalOfProfitValidator(
            ILegalTypeValidator legalTypeValidator,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            NumberValidator numberValidator,
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.legalTypeValidator = legalTypeValidator;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        public async Task ValidateAsync(WithdrawalOfProfitSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await legalTypeValidator.ValidateForIpAsync();
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
        }

        private async Task ValidateKontragentAsync(WithdrawalOfProfitSaveRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            var kontragent = await kontragentsValidator.ValidateAsync(request.Kontragent);
            if (kontragent.Form == null && (request.DocumentBaseId > 0 || request.OperationState == OperationState.Imported || request.OperationState == OperationState.Duplicate))
            {
                // todo: здесь забит костыль, ибо импорт создает неполноценных контрагентов, которых пихает в операцию
                // убрать после фикса импорта
                return;
            }
            if (kontragent.Form != KontragentForm.FL)
            {
                throw new BusinessValidationException("Contractor.Id", "Контрагент должен быть физ. лицом");
            }
        }

        private async Task ValidatePaymentNumber(WithdrawalOfProfitSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number);
            }
        }
    }
}