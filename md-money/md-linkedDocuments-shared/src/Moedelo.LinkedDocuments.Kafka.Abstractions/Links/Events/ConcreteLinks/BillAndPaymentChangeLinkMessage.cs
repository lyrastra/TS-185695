using Moedelo.Common.Kafka.Abstractions.Base;
using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по изменению связей счет-платеж 
    /// </summary>
    public class BillAndPaymentChangeLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public List<BillAndPaymentLink> DeletedLinks { get; set; }
        
        /// <summary>
        /// Созданные связи
        /// </summary>
        public List<BillAndPaymentLink> CreatedLinks { get; set; }
    }
}