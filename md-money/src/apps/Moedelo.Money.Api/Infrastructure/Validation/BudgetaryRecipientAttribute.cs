using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class BudgetaryRecipientAttribute : StringLengthAttribute
    {
        private const int maxLength = 210;

        public BudgetaryRecipientAttribute()
            : base(maxLength)
        {
            ErrorMessage = $"Длина не должена превышать {maxLength} символов.";
        }
    }
}
