using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SumValueAttribute : RangeAttribute
    {
        public const double MinValue = 0.01;
        public const double MaxValue = 1000000000;

        public SumValueAttribute(double minValue = MinValue, double maxValue = MaxValue)
            : base(minValue, maxValue)
        {
            ErrorMessage = $"Значение должно быть между {minValue} and {maxValue}.";
        }
    }
}
