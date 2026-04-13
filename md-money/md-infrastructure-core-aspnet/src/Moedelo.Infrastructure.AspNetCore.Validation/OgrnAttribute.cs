using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class OgrnAttribute : ValidationAttribute
    {
        private static readonly Regex Ogrn = new Regex(@"^\d{13}$");
        private static readonly Regex Ogrnip = new Regex(@"^\d{15}$");

        public OgrnAttribute()
            : base("Неверный формат ОГРН")
        {
        }

        public override bool IsValid(object obj)
        {
            var value = obj?.ToString();

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (IsValidOgrn(value) || IsValidOgrnip(value))
            {
                return true;
            }

            return false;
        }

        private static bool IsValidOgrn(string textValue)
        {
            if (!Ogrn.IsMatch(textValue))
            {
                return false;
            }

            var value = long.Parse(textValue);

            return value % 10 == (value / 10) % 11 % 10;
        }

        private static bool IsValidOgrnip(string textValue)
        {
            if (!Ogrnip.IsMatch(textValue))
            {
                return false;
            }

            var value = long.Parse(textValue);

            return value % 10 == (value / 10) % 13 % 10;
        }
    }
}