using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.TaxPostings
{
    internal static class PaymentFromCustomerIpOsnoPostingsGenerator
    {
        public static IpOsnoTaxPostingsResponse Generate(PaymentFromCustomerTaxPostingsGenerateRequest request)
        {
            return new IpOsnoTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = TaxationSystemType.Osno,
                Postings = new [] { new IpOsnoTaxPosting
                {
                    Date = request.Date,
                    Direction = TaxPostingDirection.Incoming,
                    Sum = request.Sum - (request.IncludeNds ? request.NdsSum.GetValueOrDefault() : 0)
                }}
            };
        }
    }
}
