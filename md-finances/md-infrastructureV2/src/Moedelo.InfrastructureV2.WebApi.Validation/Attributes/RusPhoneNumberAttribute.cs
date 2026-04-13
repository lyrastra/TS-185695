using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class RusPhoneNumberAttribute : ValidationAttribute
    {
        private static readonly Regex TrimRegex = new Regex(@"[\s-+()]+");
        private static readonly Regex PatternRegex = new Regex(@"^(7|8)\d{10}$");

        public RusPhoneNumberAttribute() : base("Телефонный номер должен начинатся с 7 или 8 и содержать в общей сложности 11 цифр")
        { }

        public override bool IsValid(object objValue)
        {
            var value = objValue?.ToString();

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            return PatternRegex.IsMatch(TrimRegex.Replace(value, ""));
        }
    }
}