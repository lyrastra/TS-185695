using System.ComponentModel.DataAnnotations;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.PaymentMethods.Dto
{
    public class CreatePaymentMethodDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Длина кода можеть быть от 3 до 50 символов.", MinimumLength = 3)]
        public string Code { get; set; }

        [Required]
        public PaymentMethodType Type { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [StringLength(250, ErrorMessage = "Длина комментария не может можеть больше 250 символов.")]
        public string Description { get; set; }
    }
}