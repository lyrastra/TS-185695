using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class ContractorNameAttribute : StringLengthAttribute
    {
        private const int MaxLength = 1000;

        public ContractorNameAttribute()
            : base(MaxLength)
        {
            ErrorMessage = $"Длина не должена превышать {MaxLength} символов.";
        }
    }
}
