using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders
{
    public sealed class PaymentOrderChangeTaxationSystemCommandMessage
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
