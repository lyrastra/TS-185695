using System.Collections.Generic;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    /// <summary>
    /// Авансовый отчет "Оплата поставщику": создание
    /// </summary>
    public class PaymentToSupplierAdvanceStatementCreatedMessage : BaseAdvanceStatementMessage
    {
        public IReadOnlyCollection<PaymentToSupplierItem> Items { get; set; }
    }
}
