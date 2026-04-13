using System;

namespace Moedelo.PaymentImport.Dto
{
    public class ReconciliationEventAppendRequestDto
    {
        public int FirmId { get; set; }
        public int SettlementAccountId { get; set; }
        public string MongoObjectId { get; set; }
        public Guid ReconcilationSessionId { get; set; }
        public DateTime ReconcilationDate { get; set; }
    }
}
