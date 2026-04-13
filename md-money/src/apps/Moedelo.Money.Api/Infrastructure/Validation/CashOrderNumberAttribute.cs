using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class CashOrderNumberAttribute : StringLengthAttribute
    {
        public CashOrderNumberAttribute(int maxLength = 255)
            : base(maxLength)
        {
            ErrorMessage = $"Длина номера не должена превышать {maxLength} символов.";
        }
    }
}
