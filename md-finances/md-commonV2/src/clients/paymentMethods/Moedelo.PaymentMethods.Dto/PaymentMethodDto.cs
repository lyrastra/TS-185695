using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.PaymentMethods.Dto
{
    public class PaymentMethodDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public PaymentMethodType Type { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}