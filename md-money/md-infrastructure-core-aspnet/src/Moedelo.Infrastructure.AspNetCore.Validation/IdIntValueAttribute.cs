using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IdIntValueAttribute : RangeAttribute
    {
        public IdIntValueAttribute()
            : this(1, int.MaxValue)
        {
        }

        public IdIntValueAttribute(int minValue, int maxValue)
            : base(minValue, maxValue)
        {
            ErrorMessage = $"Это поле должно быть заполнено значениями из диапазона от {minValue} до {maxValue}.";
        }
    }
}
