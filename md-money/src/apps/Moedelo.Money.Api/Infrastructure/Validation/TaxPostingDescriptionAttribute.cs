using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class TaxPostingDescriptionAttribute : StringLengthAttribute
    {
        private const int maxLength = 900;

        public TaxPostingDescriptionAttribute()
            : base(maxLength)
        {
            ErrorMessage = $"Длина описания не должна превышать {maxLength} символов.";
        }
    }
}
