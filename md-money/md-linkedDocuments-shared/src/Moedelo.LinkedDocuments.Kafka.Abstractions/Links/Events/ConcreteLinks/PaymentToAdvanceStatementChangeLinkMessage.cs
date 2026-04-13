using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по изменению связей платеж-авансовый отчет
    /// Пока актуально только удаление связей (расширить созданием, если потребуется) 
    /// </summary>
    public class PaymentToAdvanceStatementChangeLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public List<PaymentAndAdvanceStatementLink> DeletedLinks { get; set; }
    }
}