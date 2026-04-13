using Moedelo.AccountingStatements.Enums;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Models
{
    public class Nds
    {
        public NdsType? NdsType { get; set; }

        public decimal NdsSum { get; set; }
    }
}