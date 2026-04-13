using Moedelo.Common.Kafka.Abstractions.Base;
using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по изменению связей акт приема-передачи - платеж 
    /// </summary>
    public class ReceiptStatementAndPaymentChangeLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public List<ReceiptStatementAndPaymentLink> DeletedLinks { get; set; }

        /// <summary>
        /// Созданные связи
        /// </summary>
        public List<ReceiptStatementAndPaymentLink> CreatedLinks { get; set; }
    }
}
