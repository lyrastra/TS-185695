using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class SkypeAttribute : RegularExpressionAttribute
    {
        public SkypeAttribute()
            : base(@"^[a-zA-Z\d\.]{6,32}$")
        {
            ErrorMessage = "Неверный формат логина Skype. Допустимо значение из латинских букв, цифр и точек длиной от 6 до 32 символов.";
        }
    }
}