using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class TaxPostingClientData
    {
        public string PostingDate { get; set; }

        public decimal Incoming { get; set; }

        public decimal Outgoing { get; set; }

        public string Destination { get; set; }

        public OsnoTransferType Type { get; set; }

        public OsnoTransferKind Kind { get; set; }

        public NormalizedCostType NormalizedCostType { get; set; }

        public TaxPostingsDirection Direction => this.Incoming == 0 ? TaxPostingsDirection.Outgoing : TaxPostingsDirection.Incoming;
    }
}