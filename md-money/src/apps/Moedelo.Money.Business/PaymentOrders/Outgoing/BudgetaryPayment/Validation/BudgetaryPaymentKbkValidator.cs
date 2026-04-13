using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentKbkValidator))]
    internal class BudgetaryPaymentKbkValidator : IBudgetaryPaymentKbkValidator
    {
        private readonly IKbkReader kbkReader;

        public BudgetaryPaymentKbkValidator(IKbkReader kbkReader)
        {
            this.kbkReader = kbkReader;
        }

        public async Task<Kbk> ValidateAsync(BudgetaryAccountCodes accountCode, int? kbkId, string kbkNumber)
        {
            if (kbkId == null && accountCode == BudgetaryAccountCodes.OtherTaxes)
            {
                return null;
            }

            var kbk = await kbkReader.GetByIdAsync(kbkId ?? 0);
            if (kbk == null && accountCode == BudgetaryAccountCodes.OtherTaxes)
            {
                return null;
            }

            if (kbk == null)
            {
                throw new BusinessValidationException("KbkId", $"Не найден КБК с идентификатором {kbkId}");
            }

            if (kbk.Number != kbkNumber)
            {
                throw new BusinessValidationException("KbkNumber", $"КБК {kbkNumber} не соответствует номеру КБК с идентификатором {kbk.Id}");
            }

            return kbk;
        }
    }
}