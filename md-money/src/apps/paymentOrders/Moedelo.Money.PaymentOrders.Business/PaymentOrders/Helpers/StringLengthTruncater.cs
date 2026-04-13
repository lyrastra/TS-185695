namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Helpers
{
    public static class StringLengthTruncater
    {
        public static string Truncate(string value, int maxLength)
        {
            return value?.Length > maxLength
                ? value.Substring(0, maxLength)
                : value;
        }
    }
}
