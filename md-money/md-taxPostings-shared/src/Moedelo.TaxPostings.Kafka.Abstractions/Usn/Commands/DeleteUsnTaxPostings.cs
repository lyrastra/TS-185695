using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Usn.Commands
{
    public class DeleteUsnTaxPostings : IEntityCommandData
    {
        public long DocumentBaseId { get; set; }
    }
}
