using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Commands.CashOrders
{
    public sealed class CashOrderChangeTaxationSystemCommandMessage
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
