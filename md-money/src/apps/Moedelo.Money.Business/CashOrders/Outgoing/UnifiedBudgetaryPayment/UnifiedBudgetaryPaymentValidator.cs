using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment.Validation;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentValidator))]
    internal sealed class UnifiedBudgetaryPaymentValidator : IUnifiedBudgetaryPaymentValidator
    {
        private readonly UnifiedBudgetaryPaymentDateValidator dateValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ICashValidator cashValidator;
        private readonly UnifiedBudgetarySubPaymentsBaseDocumentsValidator baseDocumentsValidator;
        private readonly IBudgetaryPeriodValidator periodValidator;
        private readonly UnifiedBudgetarySubPaymentKbkValidator kbkValidator;
        private readonly UnifiedBudgetarySubPaymentPatentValidator patentValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly UnifiedBudgetaryPaymentTaxPostingValidator paymentTaxPostingsValidator;

        public UnifiedBudgetaryPaymentValidator(
            UnifiedBudgetaryPaymentDateValidator dateValidator,
            IClosedPeriodValidator closedPeriodValidator,
            ICashValidator cashValidator,
            UnifiedBudgetarySubPaymentsBaseDocumentsValidator baseDocumentsValidator,
            IBudgetaryPeriodValidator periodValidator,
            UnifiedBudgetarySubPaymentKbkValidator kbkValidator,
            UnifiedBudgetarySubPaymentPatentValidator patentValidator,
            TaxPostingsValidator taxPostingsValidator,
            UnifiedBudgetaryPaymentTaxPostingValidator paymentTaxPostingsValidator)
        {
            this.dateValidator = dateValidator;
            this.closedPeriodValidator = closedPeriodValidator;
            this.cashValidator = cashValidator;
            this.baseDocumentsValidator = baseDocumentsValidator;
            this.periodValidator = periodValidator;
            this.kbkValidator = kbkValidator;
            this.patentValidator = patentValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.paymentTaxPostingsValidator = paymentTaxPostingsValidator;
        }

        public async Task ValidateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            await dateValidator.ValidateAsync(request.Date);
            await closedPeriodValidator.ValidateAsync(request.Date);

            await cashValidator.ValidateAsync(request.CashId);

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

                await taxPostingsValidator.ValidateAsync(request.Date, subPayment.TaxPostings);
                await paymentTaxPostingsValidator.ValidateAsync(request);

                i++;
            }
        }
    }
}