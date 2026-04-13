using Moedelo.Common.Enums.Enums.Billing;
using System.Collections.Generic;

namespace Moedelo.PaymentMethods.Dto
{
    public class PaymentMethodSearchCriteriaDto
    {
        public IReadOnlyCollection<int> Ids { get; set; }
        public IReadOnlyCollection<string> Codes { get; set; }
        public bool? IsActive { get; set; }
        public IReadOnlyCollection<PaymentMethodType> Types { get; set; }
    }
}