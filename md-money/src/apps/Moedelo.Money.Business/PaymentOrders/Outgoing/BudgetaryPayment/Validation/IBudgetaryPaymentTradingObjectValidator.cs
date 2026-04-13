using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    internal interface IBudgetaryPaymentTradingObjectValidator
    {
        Task ValidateAsync(BudgetaryAccountCodes accountCode, int? tradingObjectId);
    }
}
