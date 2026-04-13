using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class AccPostingDescriptionAttribute : StringLengthAttribute
    {
        private const int maxLength = 1000;

        public AccPostingDescriptionAttribute()
            : base(maxLength)
        {
            ErrorMessage = $"Длина описания не должна превышать {maxLength} символов.";
        }
    }
}
