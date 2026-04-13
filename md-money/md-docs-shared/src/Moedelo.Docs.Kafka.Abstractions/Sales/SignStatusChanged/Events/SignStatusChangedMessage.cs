using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.SignStatusChanged.Events
{
    public class SignStatusChangedMessage
    {
        /// <summary>
        /// Идентификатор документа, у которого изменился признак "подписан"
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Новое значение признака подписан
        /// </summary>
        public SignStatus SignStatus { get; set; }
    }
}