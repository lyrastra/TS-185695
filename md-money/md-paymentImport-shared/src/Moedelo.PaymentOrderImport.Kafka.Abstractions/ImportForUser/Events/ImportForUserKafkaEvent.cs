using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.ImportForUser.Events
{
    public class ImportForUserKafkaEvent : IEntityEventData
    {
        public int FirmId { get; set; }

        public string FileId { get; set; }

        public int UserId { get; set; }

        public string FileName { get; set; }

        public string SecondSettlementAccount { get; set; }

        public PaymentImportSettlementAccountType SettlementAccountType { get; set; }
    }
}


