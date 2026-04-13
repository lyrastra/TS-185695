using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Commands.CashOrders
{
    /// <summary>
    /// Изменяет СНО в кассовом ордере
    /// </summary>
    public class CashOrderChangeTaxationSystemCommand
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
