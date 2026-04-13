using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class EnumValueAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Поле содержит недопустимое значение";

        public Type EnumType { get; set; }

        public bool AllowNull { get; set; }

        public EnumValueAttribute(Type enumType = null)
            : base(DefaultErrorMessage)
        {
            EnumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return AllowNull;
            }

            var type = value.GetType();
            if (!type.IsEnum)
            {
                return false;
            }

            return type.IsEnumDefined(value);
        }

        public override string FormatErrorMessage(string name)
        {
            if (EnumType == null)
            {
                return DefaultErrorMessage;
            }
            if (!EnumType.IsEnum)
            {
                return DefaultErrorMessage;
            }
            var values = string.Join(", ", Enum.GetValues(EnumType).Cast<int>());
            return $"Поле должно принимать одно из следующих допустимых значений: {values}.";
        }
    }
}
