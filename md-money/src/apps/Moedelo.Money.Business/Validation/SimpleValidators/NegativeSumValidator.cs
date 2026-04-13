namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    public static class NegativeSumValidator
    {
        public static string Validate(string memberName, decimal value)
        {
            const decimal minValue = -1000000000;
            const decimal maxValue = -0.01m;

            if (value < minValue || value > maxValue)
            {
                return $"Значение должно быть между {minValue} and {maxValue}.";
            }

            return null;
        }
    }
}
