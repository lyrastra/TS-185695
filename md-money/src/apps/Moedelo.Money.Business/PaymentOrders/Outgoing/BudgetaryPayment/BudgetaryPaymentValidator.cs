using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.Kbks;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentValidator))]
    internal sealed class BudgetaryPaymentValidator : IBudgetaryPaymentValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IBudgetaryPaymentKbkValidator budgetaryPaymentKbkValidator;
        private readonly KbkValidator kbkValidator;
        private readonly IBudgetaryPaymentTradingObjectValidator tradingObjectValidator;
        private readonly IBudgetaryPaymentTaxPostingValidator taxPostingValidator;
        private readonly IBudgetaryPaymentBaseValidator paymentBaseValidator;
        private readonly IBudgetaryPeriodValidator periodValidator;
        private readonly ILinkedCurrencyInvoicesValidator currencyInvoicesValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly NumberValidator numberValidator;
        private readonly DateTime reasonChangeDate = new DateTime(2021, 10, 1);

        public BudgetaryPaymentValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IBudgetaryPaymentKbkValidator budgetaryPaymentKbkValidator,
            KbkValidator kbkValidator,
            IBudgetaryPaymentTradingObjectValidator tradingObjectValidator,
            IBudgetaryPaymentTaxPostingValidator taxPostingValidator,
            IBudgetaryPaymentBaseValidator paymentBaseValidator,
            IBudgetaryPeriodValidator periodValidator,
            ILinkedCurrencyInvoicesValidator currencyInvoicesValidator,
            TaxPostingsValidator taxPostingsValidator,
            IPaymentOrderGetter paymentOrderGetter,
            NumberValidator numberValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.budgetaryPaymentKbkValidator = budgetaryPaymentKbkValidator;
            this.kbkValidator = kbkValidator;
            this.tradingObjectValidator = tradingObjectValidator;
            this.taxPostingValidator = taxPostingValidator;
            this.paymentBaseValidator = paymentBaseValidator;
            this.periodValidator = periodValidator;
            this.currencyInvoicesValidator = currencyInvoicesValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.paymentOrderGetter = paymentOrderGetter;
            this.numberValidator = numberValidator;
        }

        public async Task ValidateAsync(BudgetaryPaymentSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            var kbk = await budgetaryPaymentKbkValidator.ValidateAsync(request.AccountCode, request.KbkId, request.KbkNumber);
            await kbkValidator.ValidateKbkPeriodAsync(kbk, request.Date, request.Period, request.AccountCode);
            await taxPostingValidator.ValidateAsync(request);
            await tradingObjectValidator.ValidateAsync(request.AccountCode, request.TradingObjectId);
            await paymentBaseValidator.ValidateAsync(request.PaymentBase, request.Period);
            await periodValidator.ValidateAsync(request.Period);
            await currencyInvoicesValidator.ValidateAsync(request, kbk);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
            BudgetaryPaymentRecipientValidator.Validate(request.Date, request.Recipient);
            if (request.Date >= reasonChangeDate)
            {
                await paymentBaseValidator.ValidateDocumentDateAsync(request);
                await paymentBaseValidator.ValidateReasonAsync(request);
            }
        }

        private async Task ValidatePaymentNumber(BudgetaryPaymentSaveRequest request)
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
    }
}