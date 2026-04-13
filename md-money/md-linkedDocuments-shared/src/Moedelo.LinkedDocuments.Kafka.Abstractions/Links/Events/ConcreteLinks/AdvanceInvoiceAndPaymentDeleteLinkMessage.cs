using Moedelo.Common.Kafka.Abstractions.Base;
using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по удалению связей авансовой сф.
    /// </summary>
    public class AdvanceInvoiceAndPaymentDeleteLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public List<AdvanceInvoiceAndPaymentLink> DeletedLinks { get; set; }
    }
}