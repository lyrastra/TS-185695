namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    /// <summary>
    /// В ЕНП протекают проводки с пуствм описанием и стопят кафку при попытке записи проводок в базу в базе Description not null (AD-1514)
    /// Возможно этот же валидатор актуален для всех типов операций его нучно применить везде но сыкатно
    /// </summary>
    public static class TaxPostingDescriptionHardValidator
    {
        public static string Validate(string memberName, string value)
        {
            const int maxLength = 900;

            if (string.IsNullOrWhiteSpace(value))
            {
                return $"Описание не должно быть пустым.";
            }

            if (value.Length > maxLength)
            {
                return $"Длина описания не должна превышать {maxLength} символов.";
            }

            return null;
        }
    }
}
