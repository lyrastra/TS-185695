using Moedelo.Money.Business.Abstractions.Exceptions;

namespace Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment
{
    internal class UnifiedBudgetaryPaymentSumValidator
    {
        public static void Validate(decimal paymentSum, decimal subPaymentsSum)
        {
            if (paymentSum != subPaymentsSum)
            {
                throw new BusinessValidationException("SubPayments", $"Cумма по всем видам налогов/взносов должна быть равна сумме ЕНП");
            }
        }
    }
}
