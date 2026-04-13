using System;
using System.Linq;

namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    public static class EnumValidator
    {
        public static string Validate<T>(string memberName, T value) where T : Enum
        {
            var type = value.GetType();

            if (!type.IsEnumDefined(value))
            {
                var values = string.Join(", ", Enum.GetValues(type).Cast<int>());
                return $"Поле должно принимать одно из следующих допустимых значений: {values}.";
            }

            return null;
        }
    }
}
