using Moedelo.Money.Business.Abstractions.Exceptions;

namespace Moedelo.Money.Business.Validation
{
    internal static class EmployeeNameValidator
    {
        public static void Validate256(string employeeName, string fieldName)
        {
            if (string.IsNullOrEmpty(employeeName))
            {
                throw new BusinessValidationException(fieldName, "Это поле должно быть заполнено.");
            }
            const int length = 256;
            if (employeeName.Length > length)
            {
                throw new BusinessValidationException(fieldName, $"Максимальная длина должна быть не больше {length} символов");
            }
        }
    }
}
