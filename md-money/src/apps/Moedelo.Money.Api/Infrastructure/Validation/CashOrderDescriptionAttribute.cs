using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class CashOrderDescriptionAttribute : StringLengthAttribute
    {
        private const int maxLength = 1000;

        public CashOrderDescriptionAttribute()
            : base(maxLength)
        {
            ErrorMessage = $"Длина назначения не должна превышать {maxLength} символов.";
        }
    }
}
