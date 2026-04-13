using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class SettlementAccountAttribute : RegularExpressionAttribute
    {
        public SettlementAccountAttribute()
            : base("^(\\d{20}|\\d{25})$")
        {
            ErrorMessage = $"Расчетный счет должен содержать 20 или 25 цифр";
        }
    }
}
