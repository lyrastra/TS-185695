using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class BudgetaryDocNumberAttribute : RegularExpressionAttribute
    {
        public BudgetaryDocNumberAttribute()
            : base("^(\\S| ){1,15}$")
        {
            ErrorMessage = $"Номер дожен состоять из букв и цифр или '0'. Длина номера не должена превышать 15 символов.";
        }
    }
}
