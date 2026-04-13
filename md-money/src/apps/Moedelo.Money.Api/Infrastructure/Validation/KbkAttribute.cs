using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class KbkAttribute : RegularExpressionAttribute
    {
        public KbkAttribute()
            : base("^\\d{20}$")
        {
            ErrorMessage = $"КБК должен содержать 20 цифр";
        }
    }
}
