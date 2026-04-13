using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models
{
    /// <summary>
    /// Позиция авансового отчёта с типом "Оплата поставщику"
    /// </summary>
    public class PaymentToSupplierItem
    {
        /// <summary>
        /// Идентификатор документа, подтверждающего покупку
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа, подтверждающего покупку
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма принятая в отчёте 
        /// </summary>
        public decimal AcceptedSum { get; set; }
    }
}
