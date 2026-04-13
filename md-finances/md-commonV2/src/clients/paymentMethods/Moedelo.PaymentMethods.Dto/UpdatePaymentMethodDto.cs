using System.ComponentModel.DataAnnotations;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.PaymentMethods.Dto
{
    public class UpdatePaymentMethodDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public PaymentMethodType Type { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}