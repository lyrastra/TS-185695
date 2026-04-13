using Moedelo.Money.Business.Abstractions.Exceptions;

namespace Moedelo.Money.Business.Validation.Kontragents
{
    internal static class KontragentNameValidator
    {
        public static void Validate1000(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new BusinessValidationException("Contractor.Name", "Это поле должно быть заполнено.");
            }
            const int length = 1000;
            if (name.Length > length)
            {
                throw new BusinessValidationException("Contractor.Name", $"Максимальная длина должна быть не больше {length} символов");
            }
        }

        public static void Validate256(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new BusinessValidationException("Contractor.Name", "Это поле должно быть заполнено.");
            }
            const int length = 256;
            if (name.Length > length)
            {
                throw new BusinessValidationException("Contractor.Name", $"Максимальная длина должна быть не больше {length} символов");
            }
        }
    }
}
