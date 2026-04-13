using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands
{
    public sealed class SetTaxStatusCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public long Id { get; set; }

        public TaxPostingStatus? TaxStatus { get; set; }
    }
}
