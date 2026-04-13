using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class EmployeeNameAttribute : StringLengthAttribute
    {
        private const int MaxLength = 255;

        public EmployeeNameAttribute()
            : base(MaxLength)
        {
            ErrorMessage = $"Длина не должена превышать {MaxLength} символов.";
        }
    }
}
