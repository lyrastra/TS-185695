using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders
{
    /// <summary>
    /// Изменяет СНО в п/п
    /// </summary>
    public class PaymentOrderChangeTaxationSystemCommand
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
