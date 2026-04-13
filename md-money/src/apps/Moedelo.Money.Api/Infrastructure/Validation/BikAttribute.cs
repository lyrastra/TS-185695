using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class BikAttribute : RegularExpressionAttribute
    {
        public BikAttribute()
            : base("^0\\d{8}$")
        {
            ErrorMessage = $"БИК должен содержать 9 цифр и начинаться с '0'";
        }
    }
}
