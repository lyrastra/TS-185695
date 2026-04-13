using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    public static class BudgetaryPaymentTaxWidget
    {
        public static BudgetaryPaymentTaxWidgetRequest Map(BudgetaryPaymentTaxWidgetDto request)
        {
            return new BudgetaryPaymentTaxWidgetRequest
            {
                Year = request.Year,
                Quarter = request.Quarter,
                KbkId = request.KbkId,
                BudgetaryAccountCode = request.BudgetaryAccountCode
            };
        }
    }
}