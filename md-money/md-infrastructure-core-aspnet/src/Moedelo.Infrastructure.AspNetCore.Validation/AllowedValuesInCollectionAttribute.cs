using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class AllowedValuesInCollectionAttribute : ValidationAttribute
    {
        private readonly object[] allowedValues;
        
        public bool AllowNull { get; set; }
        
        public bool IgnoreCase { get; set; }

        public AllowedValuesInCollectionAttribute(params object[] allowedValues)
        {
            this.allowedValues = allowedValues;
        }
        
        public override bool IsValid(object value)
        {
            if (value == null && AllowNull)
            {
                return true;
            }

            var type = value?.GetType();
            if (type == null || type.GetInterface(nameof(IEnumerable)) == null)
            {
                return false;
            }

            var valuesForCheck = (IEnumerable) value;
            return valuesForCheck.Cast<object>().All(MatchValue);
        }

        public override string FormatErrorMessage(string name)
        {
            var values = string.Join(",", allowedValues.Select(v => EnumToInt(v)));
            return $"Список содержит не допустимое значение. Допустимые: {values}.";
        }

        private bool MatchValue(object checkValue)
        {
            var type = checkValue?.GetType();
            if (type == null)
            {
                return allowedValues == null;
            }
            
            if (type == typeof(string) && IgnoreCase)
            {
                return allowedValues
                    .Select(allowedValue => (string) allowedValue)
                    .Any(castedAllowedValue => 
                        string.Equals((string) checkValue, castedAllowedValue, StringComparison.InvariantCultureIgnoreCase));
            }
            
            dynamic dynamicValueForCheck = Convert.ChangeType(checkValue, type);
        
            return allowedValues
                .Select(allowedValue => Convert.ChangeType(allowedValue, type))
                .Any(dynamicAllowedValue => dynamicValueForCheck == (dynamic) dynamicAllowedValue);
        }

        private static object EnumToInt(object value)
        {
            return (value?.GetType().IsEnum ?? false) ? (int)value : value;
        }
    }
}