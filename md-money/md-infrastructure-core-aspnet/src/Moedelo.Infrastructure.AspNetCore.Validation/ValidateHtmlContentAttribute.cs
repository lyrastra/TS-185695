using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ValidateHtmlContentAttribute : ValidationAttribute
    {
        private const string DangerousTagsPattern = @"<\s*(body|embed|frame|script|frameset|html|iframe|img|layer|link|ilayer|meta|object|input|form|video|audio|picture|svg)\b";

        public ValidateHtmlContentAttribute()
            : base("Обнаружено потенциально опасное содержимое.")
        {
        }

        public override bool IsValid(object value)
        {
            if (value is string)
                return !Regex.IsMatch(value as string, DangerousTagsPattern, RegexOptions.IgnoreCase);
            return true;
        }
    }
}
