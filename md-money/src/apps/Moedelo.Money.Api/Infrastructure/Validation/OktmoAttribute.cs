using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class OktmoAttribute : RegularExpressionAttribute
    {
        private const string OktmoPattern = "^(\\d{8}|\\d{11})$";
        private const string OktmoErrorMessage = $"ОКТМО должен содержать 8 или 11 цифр";

        private const string OktmoPatternWithZero = "^(\\d{8}|\\d{11}|0)$";
        private const string OktmoErrorMessageWithZero = $"ОКТМО должен содержать 8 или 11 цифр или 0";

        public OktmoAttribute(bool allowZero = false)
            : base(allowZero ? OktmoPatternWithZero : OktmoPattern)
        {
            ErrorMessage = allowZero
                ? OktmoErrorMessageWithZero
                : OktmoErrorMessage;
        }
    }
}
