using System.Globalization;

namespace Moedelo.BankIntegrations.Enums
{
    /// <summary>  Очередность платежа </summary>
    public enum PaymentPriority
    {
        Default = 0,

        First = 1,

        Second = 2,

        Third = 3,

        Forth = 4,

        Fifth = 5,

        Sixth = 6
    }

    public static class PaymentPriorityExtensions
    {
        public static string ToText(this PaymentPriority type)
        {
            return type == PaymentPriority.Default
                   ? string.Empty
                   : ((int)type).ToString(CultureInfo.InvariantCulture);
        }
    }
}
