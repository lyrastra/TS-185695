using System;

namespace Moedelo.Money.Domain.PaymentOrders
{
    public struct ActualizeRequestItem
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsOutsourceApproved { get; set; }
    }
}
