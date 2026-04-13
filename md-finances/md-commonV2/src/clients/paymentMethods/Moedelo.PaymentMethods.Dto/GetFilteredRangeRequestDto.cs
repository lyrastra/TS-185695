using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.PaymentMethods.Dto
{
    public class GetFilteredRangeRequestDto
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
        /// <summary> Фильтр по части кода метода оплаты </summary>
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public PaymentMethodType? Type { get; set; }
    }
}