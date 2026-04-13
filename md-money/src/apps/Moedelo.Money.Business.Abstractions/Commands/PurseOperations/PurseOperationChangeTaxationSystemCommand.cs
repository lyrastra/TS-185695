using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Commands.PurseOperations
{
    /// <summary>
    /// Изменяет СНО в операции по платёжным системам
    /// </summary>
    public class PurseOperationChangeTaxationSystemCommand
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
