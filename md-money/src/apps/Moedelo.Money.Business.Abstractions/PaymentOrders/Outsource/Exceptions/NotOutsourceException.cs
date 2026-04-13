using System;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Exceptions
{
    public class NotOutsourceException : Exception
    {
        public NotOutsourceException() : base("Не аутсорсер")
        {
            
        }
    }
}
