using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class PaymentOrderNumberAttribute : StringLengthAttribute
    {
        public PaymentOrderNumberAttribute(int maxLength = 20)
            : base(maxLength)
        {
            ErrorMessage = $"Длина номера не должена превышать {maxLength} символов.";
        }
    }
}
