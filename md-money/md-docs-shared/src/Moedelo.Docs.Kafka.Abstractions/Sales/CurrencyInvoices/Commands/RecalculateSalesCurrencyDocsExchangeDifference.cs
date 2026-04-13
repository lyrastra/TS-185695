using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Commands
{
    /// <summary>
    /// Команда на перерасчет курсовой разницы в инвойсах на продужу при изменении валютного платежа  
    /// </summary>
    public class RecalculateSalesCurrencyDocsExchangeDifference : IEntityCommandData
    {
        /// <summary>
        /// BaseId платежаы
        /// </summary>
        public long PaymentBaseId { get; set; }
        
        /// <summary>
        /// Список BaseId инвойсов (связанных или отвязанных)
        /// </summary>
        public ISet<long> CurrencyInvoiceBaseIds { get; set; }
    }
}