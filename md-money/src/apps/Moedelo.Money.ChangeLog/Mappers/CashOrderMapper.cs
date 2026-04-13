using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.Enums;
using System;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class CashOrderMapper
    {
        public static string MapToName(this FirmCashOrderDto cashOrder)
        {
            return cashOrder != null
                ? $"{GetAbbr(cashOrder)} №{cashOrder.Number} от {(cashOrder.Date.AsLocalDate()):dd.MM.yyyy}"
                : null;
        }

        private static DateTime AsLocalDate(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local).Date;
        }

        private static string GetAbbr(FirmCashOrderDto cashOrder)
        {
            return cashOrder.Direction switch
            {
                PaymentDirection.Outgoing => "РКО",
                PaymentDirection.Incoming => "ПКО",
                _ => throw new NotSupportedException(),
            };
        }
    }
}
