namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    public static class SumValidator
    {
        public static string Validate(string memberName, decimal value)
        {
            const decimal minValue = 0.01m;
            const decimal maxValue = 1000000000;

            if (value < minValue || value > maxValue)
            {
                return $"Значение должно быть между {minValue} and {maxValue}.";
            }

            return null;
        }
    }
}
