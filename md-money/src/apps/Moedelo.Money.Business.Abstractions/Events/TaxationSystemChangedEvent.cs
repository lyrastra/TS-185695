using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Events
{
    /// <summary>
    /// Изменилась СНО у операции
    /// </summary>
    public class TaxationSystemChangedEvent
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
