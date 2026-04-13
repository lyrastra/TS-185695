using System;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Incoming.RetailRevenue
{
    internal static class RetailRevenueMapper
    {
        public static DateTime? MapSaleDate(DateTime? saleDate, DateTime date)
        {
            if (saleDate.HasValue &&
                saleDate.Value.Date != date.Date &&
                saleDate.Value.Date != DateTime.MinValue)
            {
                return saleDate;
            }
            return null;
        }
    }
}
