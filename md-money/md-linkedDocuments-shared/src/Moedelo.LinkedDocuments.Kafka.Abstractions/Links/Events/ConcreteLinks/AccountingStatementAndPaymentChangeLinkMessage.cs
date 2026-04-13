using Moedelo.Common.Kafka.Abstractions.Base;
using System.Collections.Generic;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks
{
    /// <summary>
    /// Событие по изменению связей бухсправка-платеж
    /// Пока актуально только удаление связей (расширить созданием, если потребуется) 
    /// </summary>
    public class AccountingStatementAndPaymentChangeLinkMessage : MoedeloKafkaMessageValueBase
    {
        /// <summary>
        /// Удаленные связи
        /// </summary>
        public List<AccountingStatementAndPaymentLink> DeletedLinks { get; set; }
    }
}