using System.Collections.Generic;

namespace Moedelo.PaymentMethods.Dto
{
    public class PaymentMethodsRangeDto
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyCollection<PaymentMethodDto> Data { get; set; }
    }
}