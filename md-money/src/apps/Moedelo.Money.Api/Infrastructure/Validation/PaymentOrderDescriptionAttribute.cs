using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class PaymentOrderDescriptionAttribute : StringLengthAttribute
    {
        private const int maxLength = 500;

        public PaymentOrderDescriptionAttribute()
            : base(maxLength)
        {
            ErrorMessage = $"Длина описания не должна превышать {maxLength} символов.";
        }
    }
}
