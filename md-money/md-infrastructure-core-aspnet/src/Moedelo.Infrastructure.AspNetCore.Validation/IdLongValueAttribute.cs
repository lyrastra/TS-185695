using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IdLongValueAttribute : RangeAttribute
    {
        public IdLongValueAttribute()
            : this(1, long.MaxValue)
        {
        }

        public IdLongValueAttribute(long minValue, long maxValue)
            : base(minValue, maxValue)
        {
            ErrorMessage = $"Это поле должно быть заполнено значениями из диапазона от {minValue} до {maxValue}.";
        }
    }
}
