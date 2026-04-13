using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class SkypeAttribute : ValidationAttribute
    {
        private static readonly Regex SkypeLoginRegex = new Regex(@"^[a-zA-Z\d\.]{6,32}$");

        public SkypeAttribute() : base("Неверный формат логина Skype. Допустимо значение из латинских букв, цифр и точек длиной от 6 до 32 символов")
        {
        }

        public override bool IsValid(object objValue)
        {
            var value = objValue?.ToString();

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            return SkypeLoginRegex.IsMatch(value);
        }
    }
}