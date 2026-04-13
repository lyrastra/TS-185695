using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentValidator))]
    internal sealed class UnifiedBudgetaryPaymentValidator : IUnifiedBudgetaryPaymentValidator
    {
        private readonly UnifiedBudgetaryPaymentDateValidator dateValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly UnifiedBudgetarySubPaymentsBaseDocumentsValidator baseDocumentsValidator;
        private readonly IBudgetaryPeriodValidator periodValidator;
        private readonly UnifiedBudgetarySubPaymentKbkValidator kbkValidator;
        private readonly UnifiedBudgetarySubPaymentPatentValidator patentValidator;
        private readonly TradingObjectValidator tradingObjectValidator;
        private readonly UnifiedBudgetaryPaymentTaxPostingsValidator taxPostingsValidator;
        private readonly NumberValidator numberValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public UnifiedBudgetaryPaymentValidator(
            UnifiedBudgetaryPaymentDateValidator dateValidator,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            UnifiedBudgetarySubPaymentsBaseDocumentsValidator baseDocumentsValidator,
            IBudgetaryPeriodValidator periodValidator,
            UnifiedBudgetarySubPaymentKbkValidator kbkValidator,
            UnifiedBudgetarySubPaymentPatentValidator patentValidator,
            TradingObjectValidator tradingObjectValidator,
            UnifiedBudgetaryPaymentTaxPostingsValidator taxPostingsValidator,
            NumberValidator numberValidator,
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.dateValidator = dateValidator;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.baseDocumentsValidator = baseDocumentsValidator;
            this.periodValidator = periodValidator;
            this.kbkValidator = kbkValidator;
            this.patentValidator = patentValidator;
            this.tradingObjectValidator = tradingObjectValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        public async Task ValidateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            await dateValidator.ValidateAsync(request.Date);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidatePaymentNumber(request);
            await ValidateSubPaymentsAsync(request);
        }

        private async Task ValidateSubPaymentsAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            if (request.OperationState == OperationState.NoSubPayments)
            {
                return;
            }
            
            if (request.SubPayments.Count == 0)
            {
                throw new BusinessValidationException("SubPayments", $"Необходимо указать дочерние бюджетные платежи");
            }

            var subPaymentsSum = request.SubPayments.Sum(x => x.Sum);
            UnifiedBudgetaryPaymentSumValidator.Validate(request.Sum, subPaymentsSum);

            var subPaymentDocumentIds = request.SubPayments.Select(x => x.DocumentBaseId).ToArray();
            await baseDocumentsValidator.ValidateAsync(subPaymentDocumentIds);

            var i = 0;
            foreach (var subPayment in request.SubPayments)
            {
                var prefix = $"SubPayments[{i}]";

                await periodValidator.ValidateAsync(subPayment.Period);

                var kbk = await kbkValidator.ValidateAsync(request.Date, subPayment.KbkId, subPayment.Period, prefix);
                await patentValidator.ValidateAsync(kbk, subPayment.PatentId, prefix);

                tradingObjectValidator.ValidateAsync(kbk, subPayment.TradingObjectId, prefix);

                await taxPostingsValidator.ValidateAsync(request.Date, subPayment.TaxPostings);

                i++;
            }
        }

        private async Task ValidatePaymentNumber(UnifiedBudgetaryPaymentSaveRequest request)
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