using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    /// <summary>
    /// Класс для поддержки на UI бэканда новых денег.
    /// Не использовать без острой необходимости
    /// </summary>
    public class OperationBase
    {
        public MoneyDirection Direction { get; set; }
        public OperationType OperationType { get; set; }
        public OperationKind OperationKind { get; set; }
        public long DocumentBaseId { get; set; }
    }
}