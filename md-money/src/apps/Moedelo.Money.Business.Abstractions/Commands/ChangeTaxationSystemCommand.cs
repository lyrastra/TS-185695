using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Business.Abstractions.Commands
{
    /// <summary>
    /// Изменяет СНО у операций
    /// </summary>
    public class ChangeTaxationSystemCommand
    {
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
