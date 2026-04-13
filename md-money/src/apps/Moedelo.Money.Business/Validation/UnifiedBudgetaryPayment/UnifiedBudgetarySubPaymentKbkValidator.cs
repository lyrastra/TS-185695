using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.Validation.Extensions;
using Moedelo.Money.Business.Validation.Kbks;
using Moedelo.Money.Domain.Operations;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetarySubPaymentKbkValidator))]
    internal class UnifiedBudgetarySubPaymentKbkValidator
    {
        private readonly KbkValidator kbkValidator;

        public UnifiedBudgetarySubPaymentKbkValidator(
            KbkValidator kbkValidator)
        {
            this.kbkValidator = kbkValidator;
        }

        public async Task<Kbk> ValidateAsync(
            DateTime paymentDate,
            int kbkId,
            BudgetaryPeriod period,
            string prefix = null)
        {
            var kbk = await kbkValidator.ValidateAsync(kbkId, prefix);
            await kbkValidator.ValidateKbkPeriodAsync(kbk, paymentDate, period);
            if (Enum.IsDefined(kbk.AccountCode) == false)
            {
                throw new BusinessValidationException("KbkId".WithPrefix(prefix), $"КБК с идентификатором {kbk.Id} нельзя использовать в ЕНП");
            }

            return kbk;
        }
    }
}
