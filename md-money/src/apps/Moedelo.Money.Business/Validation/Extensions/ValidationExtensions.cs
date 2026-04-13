namespace Moedelo.Money.Business.Validation.Extensions
{
    internal static class ValidationExtensions
    {
        public static string WithPrefix(this string name, string prefix = null)
        {
            return string.IsNullOrEmpty(prefix)
                ? name
                : $"{prefix}.Name";
        }
    }
}
