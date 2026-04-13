using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class BudgetaryDocDateAttribute : RegularExpressionAttribute
    {
        public BudgetaryDocDateAttribute()
            : base("^(\\d{4}-\\d{2}-\\d{2}|0)$")
        {
            ErrorMessage = $"Поле должно содержать дату или '0'";
        }
    }
}
