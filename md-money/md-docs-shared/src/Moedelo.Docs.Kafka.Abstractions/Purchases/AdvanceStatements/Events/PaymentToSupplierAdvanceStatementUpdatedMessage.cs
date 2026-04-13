using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    /// <summary>
    /// Авансовый отчет "Оплата поставщику": обновление
    /// </summary>
    public class PaymentToSupplierAdvanceStatementUpdatedMessage : BaseAdvanceStatementMessage
    {
        public IReadOnlyCollection<PaymentToSupplierItem> Items { get; set; }

        /// <summary>
        /// Тип авансового отчёта, который был до сохранения
        /// </summary>
        public AdvanceStatementType OldType { get; set; }
    }
}
