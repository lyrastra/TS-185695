using System;

namespace Moedelo.Money.Providing.Business
{
    static class DateTimeHelper
    {
        public static DateTime Max(DateTime first, DateTime second)
        {
            return second > first ? second : first;
        }
    }
}
