using Moedelo.Infrastructure.AspNetCore.Xss;
using Moedelo.Money.Business.Abstractions.Exceptions;

namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    public static class XssStringValidator
    {
        public static string Validate(string memberName, string value)
        {
            try
            {
                XssValidator.Validate(value);
                return null;
            }
            catch (XssValidationException)
            {
                throw new BusinessValidationException(memberName, "Обнаружено потенциально опасное содержимое.");
            }
        }
    }
}
