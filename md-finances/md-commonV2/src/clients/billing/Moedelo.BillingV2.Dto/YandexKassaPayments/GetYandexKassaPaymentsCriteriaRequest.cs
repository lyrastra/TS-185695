using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.YandexKassaPayments
{
    public class GetYandexKassaPaymentsCriteriaRequest
    {
        public IEnumerable<string> YandexKassaPaymentGuids { get; set; }
        public IEnumerable<int> PaymentHistoryIds { get; set; }
    }
}