using System;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount
{
    public class TransferFromAccountShortSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public bool ProvideInAccounting { get; set; }
    }
}