using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Common.Commands
{
    public class CreatePurseOperation : IEntityCommandData
    {
        public DateTime Date { get; set; }
        public int PurseId { get; set; }
        public int? KontragentId { get; set; }
        public string Comment { get; set; }
        public decimal Sum { get; set; }
        public PurseOperationType PurseOperationType { get; set; }
        public int? SettlementAccountId { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }
        public bool? IncludeNds { get; set; }
        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }
    }
}
