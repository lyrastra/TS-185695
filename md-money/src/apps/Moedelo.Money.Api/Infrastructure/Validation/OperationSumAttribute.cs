using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class OperationSumAttribute : SumValueAttribute
    {
        public OperationSumAttribute()
            : base(maxValue: 999_999_999.99)
        {
        }
    }
}
