using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models
{
    /// <summary>
    /// Позиция авансового отчёта с типом "Командировка"
    /// </summary>
    public class BusinessTripItem
    {
        /// <summary>
        /// Сумма принятая в отчёте
        /// </summary>
        public decimal AcceptedSum { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Тип СНО
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }

        public BusinessTripExpensesType Type { get; set; }
    }
}