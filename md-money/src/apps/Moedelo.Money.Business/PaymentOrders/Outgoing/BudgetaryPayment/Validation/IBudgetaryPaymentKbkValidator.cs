using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    internal interface IBudgetaryPaymentKbkValidator
    {
        Task<Kbk> ValidateAsync(BudgetaryAccountCodes accountCode, int? kbkId, string kbkNumber);
    }
}
