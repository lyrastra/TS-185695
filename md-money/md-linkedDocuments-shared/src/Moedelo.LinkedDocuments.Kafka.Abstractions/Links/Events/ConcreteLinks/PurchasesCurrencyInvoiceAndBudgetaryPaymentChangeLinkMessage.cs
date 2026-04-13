using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по изменению связей инвойс (покупки)-бюджетный платеж 
    /// </summary>
    public class PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Созданные связи
        /// </summary>
        public PurchasesCurrencyInvoiceAndBudgetaryPaymentLink[] CreatedLinks { get; set; }
        
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public PurchasesCurrencyInvoiceAndBudgetaryPaymentLink[] DeletedLinks { get; set; }
    }
}