using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class UinAttribute : RegularExpressionAttribute
    {
        public UinAttribute()
            : base("^(\\d{20}|\\d{25}|0)$")
        {
            ErrorMessage = $"УИН должен содержать 20 или 25 цифр или '0', нельзя указывать все нули подряд";
        }

        public override bool IsValid(object value)
        {
            var input = value?.ToString();
            if (input != null && Regex.IsMatch(input, "^[0]{2,}$"))
            {
                return false;
            }
            return base.IsValid(value);
        }
    }
}
