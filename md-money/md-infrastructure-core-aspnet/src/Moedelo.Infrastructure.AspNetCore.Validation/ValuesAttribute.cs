using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class ValuesAttribute : ValidationAttribute
    {
        private readonly object[] allowedValues;

        public bool AllowNull { get; set; }
        
        /// <summary>
        /// Игнорировать регистр для строковых значений 
        /// </summary>
        public bool IgnoreCase { get; set; }

        public ValuesAttribute(params object[] allowedValues)
        {
            this.allowedValues = allowedValues;
        }

        public override bool IsValid(object value)
        {
            if (value == null && AllowNull)
            {
                return true;
            }

            if (allowedValues.Any(x => MatchValue(x, value)))
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            var values = string.Join(",", allowedValues.Select(v => EnumToInt(v)));
            return $"Поле должно принимать одно из следующих допустимых значений: {values}.";
        }

        private bool MatchValue(object v1, object v2)
        {
            var type = v1?.GetType();
            if (type == null)
            {
                return v2 == null;
            }

            if (type == typeof(string) && IgnoreCase)
            {
                return string.Equals((string) v1, v2 as string, StringComparison.InvariantCultureIgnoreCase);
            }
            
            dynamic d1 = Convert.ChangeType(v1, type);
            dynamic d2 = Convert.ChangeType(v2, type);

            return d1 == d2;
        }

        private static object EnumToInt(object value)
        {
            return (value?.GetType().IsEnum ?? false) ? (int)value : value;
        }
    }
}