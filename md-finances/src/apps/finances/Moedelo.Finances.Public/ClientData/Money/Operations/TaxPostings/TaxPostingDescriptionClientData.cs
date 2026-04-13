using System;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.TaxPostings
{
    public class TaxPostingDescriptionClientData
    {
        [JsonConverter(typeof(MdDateConverter))]
        public DateTime PostingDate { get; set; }

        public decimal Incoming { get; set; }

        public decimal Outgoing { get; set; }

        public string Destination { get; set; }

        public virtual OsnoTransferType Type { get; set; }

        public virtual OsnoTransferKind Kind { get; set; }

        public virtual NormalizedCostType NormalizedCostType { get; set; }

        public TaxPostingsDirection Direction => (Incoming != 0)
            ? TaxPostingsDirection.Incoming
            : TaxPostingsDirection.Outgoing;
    }
}