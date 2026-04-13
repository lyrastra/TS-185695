namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    public static class TaxPostingDescriptionValidator
    {
        public static string Validate(string memberName, string value)
        {
            const int maxLength = 900;

            if (value == null)
            {
                return null;
            }

            if (value.Length > maxLength)
            {
                return $"Длина описания не должна превышать {maxLength} символов.";
            }

            return null;
        }
    }
}
