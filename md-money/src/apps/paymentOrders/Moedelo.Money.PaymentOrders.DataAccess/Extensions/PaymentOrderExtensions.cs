using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.Extensions
{
    static class PaymentOrderExtensions
    {
        private const int DbNumberColumnLength = 20;

        public static void NullifySparseColumns(this PaymentOrder operation)
        {
            if (operation.IsLongTermLoan == false)
            {
                operation.IsLongTermLoan = null;
            }
            if (operation.IsMediation == false)
            {
                operation.IsMediation = null;
            }
            if (operation.IsIgnoreNumber == false)
            {
                operation.IsIgnoreNumber = null;
            }
        }

        public static void TruncateStringsColumns(this PaymentOrder operation)
        {
            if (!string.IsNullOrEmpty(operation.Number) && operation.Number.Length > DbNumberColumnLength)
            {
                operation.Number = operation.Number.Substring(0, DbNumberColumnLength);
            }
        }
    }
}
