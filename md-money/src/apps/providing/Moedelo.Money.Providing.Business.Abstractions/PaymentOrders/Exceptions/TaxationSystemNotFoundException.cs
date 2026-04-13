using System;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Exceptions
{
    public class TaxationSystemNotFoundException : Exception
    {
        public TaxationSystemNotFoundException(int year)
            : base($"Taxation system not found for year {year}")
        {
        }
    }
}
