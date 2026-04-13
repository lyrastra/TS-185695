using System.Linq;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings
{
    internal  static class PaymentToSupplierIpOsnoPostingsGenerator
    {
        public static ITaxPostingsResponse<ITaxPosting> Generate(PaymentToSupplierTaxPostingsGenerateRequest request)
        {
            if (request.DocumentLinks?.Any() != true)
            {
                return new IpOsnoTaxPostingsResponse(TaxPostingStatus.NotTax)
                {
                    Message = "Не учитывается. Добавьте документ."
                };
            }
            
            return new IpOsnoTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                Message = "Расход будет учитываться при расчете налога, если факт его покупки документально подтвержден"
            };
        }
    }
}